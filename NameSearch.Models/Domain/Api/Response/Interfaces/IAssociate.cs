namespace NameSearch.Models.Domain.Api.Response
{
    /// <summary>
    /// An Associate to a person.
    /// </summary>
    public interface IAssociate
    {
        /// <summary>
        /// Gets or sets the external identifier.
        /// </summary>
        /// <value>
        /// The external identifier.
        /// </value>
        string ExternalId { get; set; }
        /// <summary>
        /// Gets or sets the Full name of the associated person.
        /// </summary>
        /// <value>
        /// The Full name of the associated person.
        /// </value>
        string Name { get; set; }
        /// <summary>
        /// Gets or sets the Relation of the associated person to the found person.
        /// </summary>
        /// <value>
        /// The Relation of the associated person to the found person.
        /// </value>
        string Relation { get; set; }
    }
}
