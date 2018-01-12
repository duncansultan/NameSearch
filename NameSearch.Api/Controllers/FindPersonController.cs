using System.Threading.Tasks;
using HttpClient.Factory;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NameSearch.Api.Controllers.Interfaces;
using NameSearch.Api.Domain.Request;
using Newtonsoft.Json;
using Serilog;

namespace NameSearch.Api.Controllers
{
    /// <summary>
    /// Controller that Execute Requests to WhitePages Premium Find Person Api
    /// </summary>
    /// <seealso cref="NameSearch.Api.Controllers.Interfaces.IFindPersonController" />
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class FindPersonController : IFindPersonController
    {
        /// <summary>
        /// The API key
        /// </summary>
        private readonly string apiKey;
        /// <summary>
        /// The base URI
        /// </summary>
        private readonly string baseUri;
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration Configuration;
        /// <summary>
        /// The serializer settings
        /// </summary>
        private readonly JsonSerializerSettings SerializerSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindPersonController"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="serializerSettings">The serializer settings.</param>
        public FindPersonController(IConfiguration configuration, JsonSerializerSettings serializerSettings)
        {
            this.Configuration = configuration;
            this.SerializerSettings = serializerSettings;

            apiKey = configuration.GetValue<string>("WhitePages:api_key");
            baseUri = configuration.GetValue<string>("WhitePages:api_url");
        }

        /// <summary>
        /// Gets the person.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// JSON Result
        /// </returns>
        /// <exception cref="System.ArgumentNullException">model</exception>
        /// <exception cref="JsonReaderException">Empty JSON result.</exception>
        [HttpGet("[controller]/[action]/{model}.{format?}")]
        public async Task<JsonResult> GetPerson(IPersonSearch model)
        {
            if (model == null)
            {
                throw new System.ArgumentNullException(nameof(model));
            }

            var uri = GetFindPersonUri(model);
            var response = await HttpRequestFactory.Get(uri);
            var responseHeaders = response.Headers;
            var json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json))
            {
                throw new JsonReaderException("Empty JSON result.");
            }

            return new JsonResult(json, SerializerSettings);
        }

        /// <summary>
        /// Gets the find person URI.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// URI String
        /// </returns>
        private string GetFindPersonUri(IPersonSearch model)
        {
            // Use the QueryBuilder to add in new items in a safe way (handles multiples and empty values)
            var qb = new QueryBuilder
            {
                { "api_key", apiKey },
                { "name", model.Name },
                { "address.street_line_1", model.Address1 },
                { "address.street_line_2", model.Address2 },
                { "address.city", model.City },
                { "address.postal_code", model.Zip },
                { "address.state_code", model.State },
                { "address.country_code", model.Country }
            };

            // Reconstruct the original URL with new query string
            var uri = baseUri + qb.ToQueryString();
            Log.Information($"GetFindPersonUri: {uri}");
            return uri;
        }
    }
}
