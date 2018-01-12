namespace NameSearch.Models.Domain
{
    /// <summary>
    /// A Phone
    /// </summary>
    public class Phone
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string ExternalId { get; set; }
        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        public string PhoneNumber { get; set; }
    }
}
