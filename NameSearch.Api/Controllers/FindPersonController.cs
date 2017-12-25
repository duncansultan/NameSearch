using System.Threading.Tasks;
using HttpClient.Factory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
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

        public FindPersonController()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            apiKey = configuration.GetSection("ApiKey").Value;
            baseUri = configuration.GetSection("BaseUri").Value;
        }

        [HttpGet("[controller]/[action]/{model}.{format?}")]
        public async Task<JsonResult> GetPerson(IPerson model)
        {
            if (model == null)
            {
                throw new System.ArgumentNullException(nameof(model));
            }
            //https://jonhilton.net/2017/01/24/retrieve-data-from-a-third-party-openweather-api-using-asp-net-core-web-api/
            var uri = GetFindPersonUri(model);
            var response = await HttpRequestFactory.Get(uri);
            var responseHeaders = response.Headers;
            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json))
            {
                throw new JsonReaderException("Empty JSON result.");
            }

            return new JsonResult(json);
        }
        
        [HttpGet("[controller]/[action]/{model}.{format?}")]
        public async Task<JsonResult> GetPerson(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new System.ArgumentNullException(nameof(name));
            }
            var uri = QueryHelpers.AddQueryString(baseUri, "api_key", apiKey);
            QueryHelpers.AddQueryString(uri, "name", name);
            var response = await HttpRequestFactory.Get(uri);
            var responseHeaders = response.Headers;
            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json))
            {
                throw new JsonReaderException("Empty JSON result.");
            }

            return new JsonResult(json);
        }

        private string GetFindPersonUri(IPerson model)
        {
            var uri = QueryHelpers.AddQueryString(baseUri, "api_key", apiKey);
            QueryHelpers.AddQueryString(uri, "name", model.Name);
            QueryHelpers.AddQueryString(uri, "address.street_line_1", model.Address1);
            QueryHelpers.AddQueryString(uri, "address.street_line_2", model.Address2);
            QueryHelpers.AddQueryString(uri, "address.city", model.City);
            QueryHelpers.AddQueryString(uri, "address.postal_code", model.Zip);
            QueryHelpers.AddQueryString(uri, "address.state_code", model.State);
            QueryHelpers.AddQueryString(uri, "address.country_code", model.Country);
            return uri;
        }
    }
}
