using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NameSearch.Models.Entities.Abstracts;
using NameSearch.Models.Entities.Interfaces;

namespace NameSearch.Models.Entities
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IEntity" />
    public class Name : AuditableEntityBase, IEntity
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
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [Required]
        public string Value { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the search priority level.
        /// </summary>
        /// <value>
        /// The search priority level.
        /// </value>
        [Range(1, 3)]
        public int SearchPriorityLevel { get; set; }
        /// <summary>
        /// Gets or sets the searches.
        /// </summary>
        /// <value>
        /// The searches.
        /// </value>
        public List<SearchTransaction> Searches { get; set; }
    }
}
