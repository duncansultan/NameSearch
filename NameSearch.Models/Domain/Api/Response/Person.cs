using System.Collections.Generic;

namespace NameSearch.Models.Domain.Api.Response
{
    /// <summary>
    /// A Person
    /// </summary>
    /// <remarks>
    /// See also https://pro.whitepages.com/developer/documentation/find-person-api/
    /// </remarks>
    public class Person
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string ExternalId { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the firstname.
        /// </summary>
        /// <value>
        /// The firstname.
        /// </value>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the middlename.
        /// </summary>
        /// <value>
        /// The middlename.
        /// </value>
        public string MiddleName { get; set; }
        /// <summary>
        /// Gets or sets the lastname.
        /// </summary>
        /// <value>
        /// The lastname.
        /// </value>
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets the age range.
        /// </summary>
        /// <value>
        /// The age range.
        /// </value>
        public string AgeRange { get; set; }
        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public string Gender { get; set; }
        /// <summary>
        /// Gets or sets the current addresses.
        /// </summary>
        /// <value>
        /// The current addresses.
        /// </value>
        public IEnumerable<Address> CurrentAddresses { get; set; } = new List<Address>();
        /// <summary>
        /// Gets or sets the historical addresses.
        /// </summary>
        /// <value>
        /// The historical addresses.
        /// </value>
        public IEnumerable<Address> HistoricalAddresses { get; set; } = new List<Address>();
        /// <summary>
        /// Gets or sets the phones.
        /// </summary>
        /// <value>
        /// The phones.
        /// </value>
        public IEnumerable<Phone> Phones { get; set; } = new List<Phone>();
        /// <summary>
        /// Gets or sets the associated people.
        /// </summary>
        /// <value>
        /// The associated people.
        /// </value>
        public IEnumerable<Associate> AssociatedPeople { get; set; } = new List<Associate>();
    }
}
