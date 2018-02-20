using NameSearch.Models.Domain;
using Newtonsoft.Json.Linq;

namespace NameSearch.App.Factories
{
    /// <summary>
    /// Search Response JObject Factory
    /// </summary>
    public static class SearchResponseJObjectFactory
    {
        /// <summary>
        /// Gets the specified search.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="jResult">The j result.</param>
        /// <returns></returns>
        public static JObject Get(Search search, JObject jResult)
        {
            var jSearch = JObject.FromObject(search);
            return new JObject
            {
                { "search", jSearch },
                { "result", jResult }
            };
        }
    }
}
