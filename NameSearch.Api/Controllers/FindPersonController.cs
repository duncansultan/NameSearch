using System;
using System.Diagnostics;
using System.Threading.Tasks;
using HttpClient.Factory;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NameSearch.Api.Controllers.Interfaces;
using NameSearch.Extensions;
using Request = NameSearch.Models.Domain.Api.Request;
using Serilog;
using NameSearch.Models.Domain.Api.Request.Interfaces;

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
        /// The logger
        /// </summary>
        private readonly ILogger logger = Log.Logger.ForContext<FindPersonController>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FindPersonController"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public FindPersonController(IConfiguration configuration)
        {
            this.Configuration = configuration;

            apiKey = configuration.GetValue<string>("WhitePages:api_key");
            baseUri = configuration.GetValue<string>("WhitePages:api_url");
        }

        /// <summary>
        /// Gets the person.
        /// </summary>
        /// <param name="person">The model.</param>
        /// <returns>
        /// JSON Result
        /// </returns>
        /// <exception cref="System.ArgumentNullException">model</exception>
        /// <exception cref="JsonReaderException">Empty JSON result.</exception>
        [HttpGet("[controller]/[action]/{model}.{format?}")]
        public async Task<Models.Utility.Interfaces.IApiResponse> GetFindPerson(IPerson person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            var log = logger.With("person", person);

            var requestUri = GetFindPersonUri(person);

            log.With("Uri", requestUri);

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var httpResponse = await HttpRequestFactory.Get(requestUri);

            stopwatch.Stop();

            var apiResponse = new Models.Utility.ApiResponse
            {
                RequestUri = requestUri,
                StatusCode = (int?) httpResponse.StatusCode,
                Content = await httpResponse.Content.ReadAsStringAsync()
            };

            log.With("apiResponse", apiResponse);

            log.InformationEvent("GetFindPerson", "Request completed in {ms}ms with HTTP Status Code {statusCode}", stopwatch.ElapsedMilliseconds, apiResponse.StatusCode);

            return apiResponse;
        }

        /// <summary>
        /// Gets the find person URI.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// URI String
        /// </returns>
        private string GetFindPersonUri(Request.Interfaces.IPerson model)
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
            return uri;
        }
    }
}
