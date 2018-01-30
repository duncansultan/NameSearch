using Newtonsoft.Json;

namespace NameSearch.Models.Domain.Api.Response
{
    /// <summary>
    /// An Associate to a person.
    /// </summary>
    /// <remarks>
    /// See also https://pro.whitepages.com/developer/documentation/find-person-api/
    /// </remarks>
    [JsonObject("associated_people")]
    public class Associate : IAssociate
    {
        /// <summary>
        /// Gets or sets the external identifier.
        /// </summary>
        /// <value>
        /// The external identifier.
        /// </value>
        [JsonProperty("id")]
        public string ExternalId { get; set; }
        /// <summary>
        /// Gets or sets the Full name of the associated person.
        /// </summary>
        /// <value>
        /// The Full name of the associated person.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the Relation of the associated person to the found person.
        /// </summary>
        /// <value>
        /// The Relation of the associated person to the found person.
        /// </value>
        [JsonProperty("relation")]
        public string Relation { get; set; }
    }
}
