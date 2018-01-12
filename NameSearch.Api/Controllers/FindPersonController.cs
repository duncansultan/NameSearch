using System.Threading.Tasks;
using HttpClient.Factory;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NameSearch.Api.Controllers.Interfaces;
using Newtonsoft.Json;

namespace NameSearch.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class FindPersonController : IFindPersonController
    {
        private readonly string apiKey;
        private readonly string baseUri;
        private readonly IConfiguration Configuration;
        private readonly JsonSerializerSettings SerializerSettings;

        public FindPersonController(IConfiguration configuration, JsonSerializerSettings serializerSettings)
        {
            this.Configuration = configuration;
            this.SerializerSettings = serializerSettings;

            apiKey = configuration.GetValue<string>("WhitePages:api_key");
            baseUri =  configuration.GetValue<string>("WhitePages:api_url");
        }

        [HttpGet("[controller]/[action]/{model}.{format?}")]
        public async Task<JsonResult> GetPerson(IPerson model)
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

        private string GetFindPersonUri(IPerson model)
        {
            // Use the QueryBuilder to add in new items in a safe way (handles multiples and empty values)
            var qb = new QueryBuilder
            {
                { "api_key", apiKey },
                { "payerId", "pyr_" },
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
