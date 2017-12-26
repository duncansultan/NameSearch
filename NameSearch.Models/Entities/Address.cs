using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NameSearch.Models.Entities.Abstracts;

namespace NameSearch.Models.Entities
{
    /// <inheritdoc />
    public class Address : EntityBase<Address>
    {
        /// <summary>
        /// Gets or sets the address1.
        /// </summary>
        /// <value>
        /// The address1.
        /// </value>
        [Required]
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
        [Required]
        public string City { get; set; }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [Required]
        public string State { get; set; }
        /// <summary>
        /// Gets or sets the zip.
        /// </summary>
        /// <value>
        /// The zip.
        /// </value>
        [Required]
        public string Zip { get; set; }
        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [DefaultValue("US")]
        public string Country { get; set; } = "US";
        /// < summary >
        /// Gets or sets the person identifier.
        /// </summary>
        /// <value>
        /// The person identifier.
        /// </value>
        [ForeignKey("PersonForeignKey")]
        public long PersonId { get; set; }
        /// <summary>
        /// Gets or sets the person object.
        /// </summary>
        /// <value>
        /// The person object.
        /// </value>
        [InverseProperty("Addresses")]
        public Person Person { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is current.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is current; otherwise, <c>false</c>.
        /// </value>
        //[DefaultValue(false)]
        public bool IsCurrent { get; set; }
        /// <summary>
        /// Gets or sets the plus4.
        /// </summary>
        /// <value>
        /// The plus4.
        /// </value>
        public string Plus4 { get; set; }
        /// <summary>
        /// Gets or sets the lattitue.
        /// </summary>
        /// <value>
        /// The lattitue.
        /// </value>
        public double? Lattitue { get; set; }
        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public double? Longitude { get; set; }

        #region Equality

        public override bool Equals(Address other)
        {
            if (other == null) return false;
            return PersonId == other.PersonId &&
                Person.Equals(other.Person) &&
                IsCurrent == other.IsCurrent &&
                string.Equals(Plus4, other.Plus4) &&
                Lattitue == other.Lattitue &&
                Longitude == other.Longitude;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (this is null) return false;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as Address);
        }

        /// <summary>
        /// Return Base Implementation.
        /// "You should only override GetHashCode if your objects are immutable."
        /// See also http://www.aaronstannard.com/overriding-equality-in-dotnet/
        /// See also https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode/263416#263416
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => base.GetHashCode();

        #endregion
    }
}
