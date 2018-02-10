using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using NameSearch.Models.Entities.Abstracts;

namespace NameSearch.Models.Entities
{
    /// <summary>
    /// Search Request Entity
    /// </summary>
    /// <seealso cref="NameSearch.Models.Entities.Abstracts.EntityBase{NameSearch.Models.Entities.PersonSearchRequest}" />
    /// <inheritdoc />
    public class PersonSearchRequest : EntityBase<PersonSearchRequest>
    {
        /// <summary>
        /// Gets or sets the search job identifier.
        /// </summary>
        /// <value>
        /// The search job identifier.
        /// </value>
        [ForeignKey("PersonSearchJobForeignKey")]
        public long PersonSearchJobId { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the address1.
        /// </summary>
        /// <value>
        /// The address1.
        /// </value>
        public string Address1 { get; set; }
        /// <summary>
        /// Gets or sets the address2.
        /// </summary>
        /// <value>
        /// The address2.
        /// </value>
        public string Address2 { get; set; }
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State { get; set; }
        /// <summary>
        /// Gets or sets the zip.
        /// </summary>
        /// <value>
        /// The zip.
        /// </value>
        public string Zip { get; set; }
        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is processed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is processed; otherwise, <c>false</c>.
        /// </value>
        public bool IsProcessed { get; set; }
        /// <summary>
        /// Gets or sets the person search results.
        /// </summary>
        /// <value>
        /// The person search results.
        /// </value>
        public List<PersonSearchResult> PersonSearchResults { get; set; }

        #region Equality

        /// <summary>
        /// See http://www.aaronstannard.com/overriding-equality-in-dotnet/
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(PersonSearchRequest other)
        {
            if (other == null) return false;
            return PersonSearchJobId == other.PersonSearchJobId &&
                string.Equals(Name, other.Name, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(Address1, other.Address1, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(Address2, other.Address2, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(State, other.State, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(Zip, other.Zip, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(Country, other.Country, StringComparison.CurrentCultureIgnoreCase) &&
                IsProcessed == other.IsProcessed &&
                (PersonSearchResults ?? new List<PersonSearchResult>()).Equals(other.PersonSearchResults);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (this is null) return false;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as PersonSearchResult);
        }

        /// <summary>
        /// Return Base Implementation.
        /// "You should only override GetHashCode if your objects are immutable."
        /// See also http://www.aaronstannard.com/overriding-equality-in-dotnet/
        /// See also https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode/263416#263416
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() => base.GetHashCode();

        #endregion
    }
}
