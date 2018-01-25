﻿namespace NameSearch.Models.Domain.Api.Response
{
    /// <summary>
    /// An Address
    /// </summary>
    public interface IAddress
    {
        /// <summary>
        /// Gets or sets the external identifier.
        /// </summary>
        /// <value>
        /// The external identifier.
        /// </value>
        string ExternalId { get; set; }
        /// <summary>
        /// Gets or sets the type of the location.
        /// </summary>
        /// <value>
        /// The type of the location.
        /// </value>
        string LocationType { get; set; }
        /// <summary>
        /// Gets or sets the street line1.
        /// </summary>
        /// <value>
        /// The street line1.
        /// </value>
        string StreetLine1 { get; set; }
        /// <summary>
        /// Gets or sets the street line2.
        /// </summary>
        /// <value>
        /// The street line2.
        /// </value>
        string StreetLine2 { get; set; }
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        string City { get; set; }
        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>
        /// The postal code.
        /// </value>
        string PostalCode { get; set; }
        /// <summary>
        /// Gets or sets the zip4.
        /// </summary>
        /// <value>
        /// The zip4.
        /// </value>
        string Zip4 { get; set; }
        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        /// <value>
        /// The state code.
        /// </value>
        string StateCode { get; set; }
        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>
        /// The country code.
        /// </value>
        string CountryCode { get; set; }
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        double? Latitude { get; set; }
        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        double? Longitude { get; set; }
        /// <summary>
        /// Gets or sets the accuracy.
        /// </summary>
        /// <value>
        /// The accuracy.
        /// </value>
        string Accuracy { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        bool IsActive { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [delivery point].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [delivery point]; otherwise, <c>false</c>.
        /// </value>
        bool DeliveryPoint { get; set; }
    }
}
