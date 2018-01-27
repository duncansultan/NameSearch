using NameSearch.Models.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace NameSearch.Models.Domain.Api.Response
{
    /// <summary>
    /// A Person
    /// </summary>
    /// <remarks>
    /// See also https://pro.whitepages.com/developer/documentation/find-person-api/
    /// </remarks>
    //[JsonConverter(typeof(PersonConverter))]
    public class Person : IPerson
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
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the firstname.
        /// </summary>
        /// <value>
        /// The firstname.
        /// </value>
        [JsonProperty("firstname")]
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the middlename.
        /// </summary>
        /// <value>
        /// The middlename.
        /// </value>
        [JsonProperty("middlename")]
        public string MiddleName { get; set; }
        /// <summary>
        /// Gets or sets the lastname.
        /// </summary>
        /// <value>
        /// The lastname.
        /// </value>
        [JsonProperty("lastname")]
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets the age range.
        /// </summary>
        /// <value>
        /// The age range.
        /// </value>
        [JsonProperty("age_range")]
        public string AgeRange { get; set; }
        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        [JsonProperty("gender")]
        public string Gender { get; set; }
        /// <summary>
        /// Gets or sets the current addresses.
        /// </summary>
        /// <value>
        /// The current addresses.
        /// </value>
        [JsonProperty("current_addresses")]
        public IEnumerable<IAddress> CurrentAddresses { get; set; } = new List<Address>();
        /// <summary>
        /// Gets or sets the historical addresses.
        /// </summary>
        /// <value>
        /// The historical addresses.
        /// </value>
        [JsonProperty("historical_addresses")]
        public IEnumerable<IAddress> HistoricalAddresses { get; set; } = new List<Address>();
        /// <summary>
        /// Gets or sets the phones.
        /// </summary>
        /// <value>
        /// The phones.
        /// </value>
        [JsonProperty("phones")]
        public IEnumerable<IPhone> Phones { get; set; } = new List<Phone>();
        /// <summary>
        /// Gets or sets the associated people.
        /// </summary>
        /// <value>
        /// The associated people.
        /// </value>
        [JsonProperty("associated_people")]
        public IEnumerable<IAssociate> AssociatedPeople { get; set; } = new List<Associate>();
    }
}
