using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NameSearch.Models.Entities.Abstracts;
using NameSearch.Models.Entities.Interfaces;

namespace NameSearch.Models.Entities
{
    /// <summary>
    /// </summary>
    /// <seealso cref="T:NetCoreSqlLite.Models.Entities.IEntity" />
    public class Address : AddressBase, IEntity
    {
        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        /// <value>
        /// The person identifier.
        /// </value>
        [ForeignKey("PersonForeignKey")]
        public long PersonId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is current.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is current; otherwise, <c>false</c>.
        /// </value>
        [DefaultValue(false)]
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
        public float? Lattitue { get; set; }
        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public float? Longitude { get; set; }
    }
}
