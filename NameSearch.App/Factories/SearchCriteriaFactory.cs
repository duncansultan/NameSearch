using NameSearch.Models.Domain;

namespace NameSearch.App.Factories
{
    /// <summary>
    /// Factory for SearchCritera model
    /// </summary>
    public static class SearchCriteriaFactory
    {
        /// <summary>
        /// Gets the specified city.
        /// </summary>
        /// <param name="maxRuns">The maximum runs.</param>
        /// <param name="city">The city.</param>
        /// <param name="state">The state.</param>
        /// <param name="zip">The zip.</param>
        /// <returns></returns>
        public static SearchCriteria Get(int maxRuns, string city, string state, string zip)
        {
            return new SearchCriteria
            {
                MaxRuns = maxRuns,
                Address1 = string.Empty,
                Address2 = string.Empty,
                City = city,
                State = state,
                Zip = zip,
                Country = "US"
            };
        }
    }
}
