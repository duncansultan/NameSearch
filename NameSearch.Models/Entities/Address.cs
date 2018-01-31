using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NameSearch.Models.Entities.Abstracts;

namespace NameSearch.Models.Entities
{
    /// <summary>
    /// Address Entity
    /// </summary>
    /// <seealso cref="NameSearch.Models.Entities.Abstracts.EntityBase{NameSearch.Models.Entities.Address}" />
    /// <inheritdoc />
    public class Address : EntityBase<Address>
    {
        /// <summary>
        /// Gets or sets the external identifier.
        /// </summary>
        /// <value>
        /// The external identifier.
        /// </value>
        public string ExternalId { get; set; }
        /// <summary>
        /// Gets or sets the type of the location.
        /// </summary>
        /// <value>
        /// The type of the location.
        /// </value>
        public string LocationType { get; set; }
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
        /// Gets or sets the plus4.
        /// </summary>
        /// <value>
        /// The plus4.
        /// </value>
        public string Plus4 { get; set; }
        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [DefaultValue("US")]
        public string Country { get; set; } = "US";
        /// <summary>
        /// Gets or sets a value indicating whether this instance is current.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is current; otherwise, <c>false</c>.
        /// </value>
        public double? Latitude { get; set; }
        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public double? Longitude { get; set; }
        /// <summary>
        /// Gets or sets the accuracy.
        /// </summary>
        /// <value>
        /// The accuracy.
        /// </value>
        public string Accuracy { get; set; }
        //[DefaultValue(false)]
        /// <summary>
        /// Gets or sets a value indicating whether this instance is current.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is current; otherwise, <c>false</c>.
        /// </value>
        public bool IsCurrent { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is historical.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is historical; otherwise, <c>false</c>.
        /// </value>
        public bool IsHistorical { get; set; }
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
        public override bool Equals(Address other)
        {
            if (other == null) return false;
            return string.Equals(ExternalId, other.ExternalId) &&
                string.Equals(LocationType, other.LocationType) &&
                string.Equals(Address1, other.Address1) &&
                string.Equals(Address2, other.Address2) &&
                string.Equals(City, other.City) &&
                string.Equals(State, other.State) &&
                string.Equals(Zip, other.Zip) &&
                string.Equals(Plus4, other.Plus4) &&
                string.Equals(Country, other.Country) &&
                Latitude == other.Latitude &&
                Longitude == other.Longitude &&
                Accuracy == other.Accuracy &&
                IsCurrent == other.IsCurrent &&
                IsHistorical == other.IsHistorical &&
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
            return Equals(obj as Address);
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
