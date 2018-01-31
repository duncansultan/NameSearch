using System.Collections.Generic;

namespace NameSearch.Models.Domain.Api.Response.Interfaces
{
    /// <summary>
    /// A Person
    /// </summary>
    /// <remarks>
    /// See also https://pro.whitepages.com/developer/documentation/find-person-api/
    /// </remarks>
    public interface IPerson
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        string ExternalId { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }
        /// <summary>
        /// Gets or sets the firstname.
        /// </summary>
        /// <value>
        /// The firstname.
        /// </value>
        string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the middlename.
        /// </summary>
        /// <value>
        /// The middlename.
        /// </value>
        string MiddleName { get; set; }
        /// <summary>
        /// Gets or sets the lastname.
        /// </summary>
        /// <value>
        /// The lastname.
        /// </value>
        string LastName { get; set; }
        /// <summary>
        /// Gets or sets the age range.
        /// </summary>
        /// <value>
        /// The age range.
        /// </value>
        string AgeRange { get; set; }
        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        string Gender { get; set; }
        /// <summary>
        /// Gets or sets the current addresses.
        /// </summary>
        /// <value>
        /// The current addresses.
        /// </value>
        IEnumerable<CurrentAddress> CurrentAddresses { get; set; }
        /// <summary>
        /// Gets or sets the historical addresses.
        /// </summary>
        /// <value>
        /// The historical addresses.
        /// </value>
        IEnumerable<HistoricalAddress> HistoricalAddresses { get; set; }
        /// <summary>
        /// Gets or sets the phones.
        /// </summary>
        /// <value>
        /// The phones.
        /// </value>
        IEnumerable<Phone> Phones { get; set; }
        /// <summary>
        /// Gets or sets the associated people.
        /// </summary>
        /// <value>
        /// The associated people.
        /// </value>
        IEnumerable<Associate> AssociatedPeople { get; set; }
    }
}
