using NameSearch.Models.Domain;
using Newtonsoft.Json.Linq;

namespace NameSearch.App.Factories
{
    /// <summary>
    /// Factory to Create Search Result Entity from JObject
    /// </summary>
    public static class PersonSearchResultFactory
    {
        /// <summary>
        /// Creates the specified person search request identifier.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="httpStatusCode">The HTTP status code.</param>
        /// <param name="jObject">The json object.</param>
        /// <returns></returns>
        public static Models.Entities.PersonSearch Create(Search search, int? httpStatusCode, JObject jObject)
        {
            //todo add search critera here
            var personSearch = new Models.Entities.PersonSearch();
            personSearch.HttpStatusCode = httpStatusCode;
            personSearch.NumberOfResults = (int)jObject["count_person"].ToObject<int>();
            personSearch.Warnings = (string)jObject["warnings"].ToString();
            personSearch.Error = (string)jObject["error"].ToString();
            personSearch.Data = jObject.ToString();
            return personSearch;
        }
    }
}