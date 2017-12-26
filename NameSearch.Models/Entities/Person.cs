using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NameSearch.Models.Entities.Abstracts;
using NameSearch.Models.Entities.Interfaces;

namespace NameSearch.Models.Entities
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:NetCoreSqlLite.Models.Entities.IEntity" />
    public class Person : AuditableEntityBase, IEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public long Id { get; set; }
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
        public List<Address> Addresses { get; set; } = new List<Address>();
    }
}
