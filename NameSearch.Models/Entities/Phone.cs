using NameSearch.Models.Entities.Abstracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace NameSearch.Models.Entities
{
    /// <summary>
    /// Phone Entity
    /// </summary>
    /// <seealso cref="NameSearch.Models.Entities.Abstracts.EntityBase{NameSearch.Models.Entities.Phone}" />
    /// <inheritdoc />
    public class Phone : EntityBase<Phone>
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
        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        /// <value>
        /// The person identifier.
        /// </value>
        [ForeignKey("PersonForeignKey")]
        public long PersonId { get; set; }

        #region Equality

        /// <summary>
        /// See http://www.aaronstannard.com/overriding-equality-in-dotnet/
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(Phone other)
        {
            if (other == null) return false;
            return string.Equals(ExternalId, other.ExternalId) &&
                string.Equals(PhoneNumber, other.PhoneNumber) &&
                PersonId == other.PersonId;
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
