using NameSearch.Models.Converters;
using Newtonsoft.Json;

namespace NameSearch.Models.Domain.Api.Response
{
    /// <summary>
    /// An Address
    /// </summary>
    /// <remarks>
    /// See also https://pro.whitepages.com/developer/documentation/find-person-api/
    /// </remarks>
    //[JsonConverter(typeof(AddressConverter))]
    public class Address : IAddress
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
        /// Gets or sets the type of the location.
        /// </summary>
        /// <value>
        /// The type of the location.
        /// </value>
        [JsonProperty("location_type")]
        public string LocationType { get; set; }
        /// <summary>
        /// Gets or sets the street line1.
        /// </summary>
        /// <value>
        /// The street line1.
        /// </value>
        [JsonProperty("street_line_1")]
        public string StreetLine1 { get; set; }
        /// <summary>
        /// Gets or sets the street line2.
        /// </summary>
        /// <value>
        /// The street line2.
        /// </value>
        [JsonProperty("street_line_2")]
        public string StreetLine2 { get; set; }
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [JsonProperty("city")]
        public string City { get; set; }
        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>
        /// The postal code.
        /// </value>
        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }
        /// <summary>
        /// Gets or sets the zip4.
        /// </summary>
        /// <value>
        /// The zip4.
        /// </value>
        [JsonProperty("zip4")]
        public string Zip4 { get; set; }
        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        /// <value>
        /// The state code.
        /// </value>
        [JsonProperty("state_code")]
        public string StateCode { get; set; }
        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>
        /// The country code.
        /// </value>
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        [JsonProperty("lat_long.latitude")]
        public double? Latitude { get; set; }
        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        [JsonProperty("lat_long.longitude")]
        public double? Longitude { get; set; }
        /// <summary>
        /// Gets or sets the accuracy.
        /// </summary>
        /// <value>
        /// The accuracy.
        /// </value>
        [JsonProperty("lat_long.accuracy")]
        public string Accuracy { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("is_active")]
        public bool IsActive { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [delivery point].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [delivery point]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("delivery_point")]
        public bool DeliveryPoint { get; set; }
    }
}
