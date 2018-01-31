namespace NameSearch.Models.Domain.Api.Response.Interfaces
{
    /// <summary>
    /// A Phone
    /// </summary>
    public interface IPhone
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        string ExternalId { get; set; }
        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        string PhoneNumber { get; set; }
    }
}
