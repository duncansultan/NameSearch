using NameSearch.Models.Domain;
using System.Collections.Generic;
using System.Linq;

namespace NameSearch.App.Factories
{
    /// <summary>
    /// Factory for Searches model
    /// </summary>
    public static class SearchesFactory
    {
        /// <summary>
        /// Gets the specified search criteria.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="names">The names.</param>
        /// <returns></returns>
        public static IEnumerable<Search> Get(SearchCriteria searchCriteria, IEnumerable<string> names)
        {
            return names.Select(x => new Search
            {
                Criteria = searchCriteria,
                Name = x
            }).ToList();
        }
    }
}
