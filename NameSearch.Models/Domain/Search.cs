namespace NameSearch.Models.Domain
{
    /// <summary>
    /// Search
    /// </summary>
    public class Search
    {
        /// <summary>
        /// Gets or sets the criteria.
        /// </summary>
        /// <value>
        /// The criteria.
        /// </value>
        public SearchCriteria Criteria { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
    }
}
