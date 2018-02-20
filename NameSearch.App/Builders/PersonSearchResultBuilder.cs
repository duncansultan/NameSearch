using NameSearch.Models.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NameSearch.App.Builders
{
    /// <summary>
    /// Factory to Create Search Result Entity from JObject
    /// </summary>
    public class PersonSearchResultBuilder
    {
        /// <summary>
        /// The serializer settings
        /// </summary>
        private JsonSerializerSettings SerializerSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonSearchResultBuilder"/> class.
        /// </summary>
        /// <param name="serializerSettings">The serializer settings.</param>
        public PersonSearchResultBuilder(JsonSerializerSettings serializerSettings)
        {
            this.SerializerSettings = serializerSettings;
        }

        /// <summary>
        /// Creates the specified person search request identifier.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="httpStatusCode">The HTTP status code.</param>
        /// <param name="jObject">The json object.</param>
        /// <returns></returns>
        public Models.Entities.PersonSearch Create(Search search, int? httpStatusCode, JObject jObject)
        {
            var personSearch = new Models.Entities.PersonSearch
            {
                Name = search.Name,
                Address1 = search.Criteria.Address1,
                Address2 = search.Criteria.Address2,
                City = search.Criteria.City,
                State = search.Criteria.State,
                Zip = search.Criteria.Zip,
                HttpStatusCode = httpStatusCode,
                Data = jObject.ToString()
            };
            personSearch.NumberOfResults = (int)jObject["count_person"].ToObject<int>();
            personSearch.Warnings = (string)jObject["warnings"].ToString();
            personSearch.Error = (string)jObject["error"].ToString();

            return personSearch;
        }

        /// <summary>
        /// Creates the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="jObject">The j object.</param>
        /// <returns></returns>
        public Models.Entities.PersonSearch Create(string fileName, JObject jObject)
        {
            var personSearch = new Models.Entities.PersonSearch
            {
                HttpStatusCode = 200
            };

            var searchJToken = jObject["search"];
            var resultJToken = jObject["result"];

            if (searchJToken != null && resultJToken != null)
            {
                personSearch.Name = (string)searchJToken["Name"];
                personSearch.Address1 = (string)searchJToken["Address1"];
                personSearch.Address2 = (string)searchJToken["Address2"];
                personSearch.City = (string)searchJToken["City"];
                personSearch.State = (string)searchJToken["State"];
                personSearch.Zip = (string)searchJToken["Zip"];
                personSearch.Data = resultJToken.ToString();
                personSearch.NumberOfResults = (int)jObject["count_person"].ToObject<int>();
                personSearch.Warnings = (string)jObject["warnings"].ToString();
                personSearch.Error = (string)jObject["error"].ToString();
            }
            else
            {
                personSearch.Name = fileName;
                personSearch.NumberOfResults = (int)jObject["count_person"].ToObject<int>();
                personSearch.Warnings = (string)jObject["warnings"].ToString();
                personSearch.Error = (string)jObject["error"].ToString();
                personSearch.Data = jObject.ToString();
            }

            return personSearch;
        }
    }
}