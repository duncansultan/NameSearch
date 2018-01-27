using NameSearch.Models.Converters;
using Newtonsoft.Json;

namespace NameSearch.Models.Domain.Api.Response
{
    /// <summary>
    /// A Phone
    /// </summary>
    /// <remarks>
    /// See also https://pro.whitepages.com/developer/documentation/find-person-api/
    /// </remarks>
    //[JsonConverter(typeof(PhoneConverter))]
    public class Phone : IPhone
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty("id")]
        public string ExternalId { get; set; }
        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Gets or sets the type of the line.
        /// </summary>
        /// <value>
        /// The type of the line.
        /// </value>
        [JsonProperty("line_type")]
        public string LineType { get; set; }
    }
}
