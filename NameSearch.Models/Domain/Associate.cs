namespace NameSearch.Models.Domain
{
    /// <summary>
    /// An Associate to a person.
    /// </summary>
    public class Associate
    {
        /// <summary>
        /// Gets or sets the external identifier.
        /// </summary>
        /// <value>
        /// The external identifier.
        /// </value>
        public string ExternalId { get; set; }
        /// <summary>
        /// Gets or sets the Full name of the associated person.
        /// </summary>
        /// <value>
        /// The Full name of the associated person.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the Relation of the associated person to the found person.
        /// </summary>
        /// <value>
        /// The Relation of the associated person to the found person.
        /// </value>
        public string Relation { get; set; }
    }
}
