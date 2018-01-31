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
        /// <param name="personSearchRequestId">The person search request identifier.</param>
        /// <param name="httpStatusCode">The HTTP status code.</param>
        /// <param name="jObject">The json object.</param>
        /// <returns></returns>
        public static Models.Entities.PersonSearchResult Create(long personSearchRequestId, int? httpStatusCode, JObject jObject)
        {
            var personSearchResult = new Models.Entities.PersonSearchResult();
            personSearchResult.PersonSearchRequestId = personSearchRequestId;
            personSearchResult.HttpStatusCode = httpStatusCode;
            personSearchResult.NumberOfResults = (int)jObject["count_person"].ToObject<int>();
            personSearchResult.Warnings = (string)jObject["warnings"].ToString();
            personSearchResult.Error = (string)jObject["error"].ToString();
            personSearchResult.Data = jObject.ToString();
            return personSearchResult;
        }
    }
}