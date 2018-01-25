﻿using NameSearch.Models.Converters;
using Newtonsoft.Json;

namespace NameSearch.Models.Domain.Api.Response
{
    /// <summary>
    /// A Phone
    /// </summary>
    [JsonConverter(typeof(PhoneConverter))]
    public class Phone : IPhone
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
