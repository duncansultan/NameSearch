using Newtonsoft.Json;

namespace NameSearch.Models.Domain.Api.Response
{
    /// <summary>
    /// Latitude and longitude associated with the returned current address. Includes “accuracy” string that represents the accuracy of the latitude/longitude with levels decreasing in precision in the following order: RoofTop, Street, PostalCode, Neighborhood, City, State, Country.
    /// It is possible for location_type and lat_long accuracy to have varying levels of precision.For example, the location_type may be “Address” while the lat_long provided is mapped only to the location of the “City” of the given address.
    /// </summary>
    [JsonObject("lat_long")]
    public class LatLong
    {
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        [JsonProperty("latitude")]
        public double? Latitude { get; set; }
        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        [JsonProperty("longitude")]
        public double? Longitude { get; set; }
        /// <summary>
        /// Gets or sets the accuracy.
        /// </summary>
        /// <value>
        /// The accuracy.
        /// </value>
        [JsonProperty("accuracy")]
        public string Accuracy { get; set; }
    }
}
