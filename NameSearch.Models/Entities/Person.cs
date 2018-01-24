using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NameSearch.Models.Entities.Abstracts;

namespace NameSearch.Models.Entities
{
    /// <summary>
    /// Person Entity
    /// </summary>
    /// <seealso cref="NameSearch.Models.Entities.Abstracts.EntityBase{NameSearch.Models.Entities.Person}" />
    /// <inheritdoc />
    public class Person : EntityBase<Person>
    {
        /// <summary>
        /// Gets or sets the person search result identifier.
        /// </summary>
        /// <value>
        /// The person search result identifier.
        /// </value>
        [ForeignKey("PersonSearchResultForeignKey")]
        public long PersonSearchResultId { get; set; }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Required]
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        /// <value>
        /// The alias.
        /// </value>
        public string Alias { get; set; }
        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>
        /// The age.
        /// </value>
        public int Age { get; set; }
        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        /// <value>
        /// The addresses.
        /// </value>
        public List<Address> Addresses { get; set; }
        /// <summary>
        /// Gets or sets the phones.
        /// </summary>
        /// <value>
        /// The phones.
        /// </value>
        public List<Phone> Phones { get; set; }
        /// <summary>
        /// Gets or sets the associates.
        /// </summary>
        /// <value>
        /// The associates.
        /// </value>
        public List<Associate> Associates { get; set; }

        #region Equality

        /// <summary>
        /// See http://www.aaronstannard.com/overriding-equality-in-dotnet/
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(Person other)
        {
            if (other == null) return false;
            return PersonSearchResultId == other.PersonSearchResultId &&
                string.Equals(FirstName, other.FirstName) &&
                string.Equals(LastName, other.LastName) &&
                string.Equals(Alias, other.Alias) &&
                Age == other.Age &&
                (Addresses ?? new List<Address>()).Equals(other.Addresses) &&
                (Phones ?? new List<Phone>()).Equals(other.Phones) &&
                (Associates ?? new List<Associate>()).Equals(other.Associates);
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
            return Equals(obj as Person);
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
