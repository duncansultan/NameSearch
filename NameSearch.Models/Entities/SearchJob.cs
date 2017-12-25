using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NameSearch.Models.Entities.Abstracts;
using NameSearch.Models.Entities.Interfaces;

namespace NameSearch.Models.Entities
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:NameSearch.Models.Entities.Interfaces.IEntity" />
    public class SearchJob : AuditableEntityBase, IEntity
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
        /// Gets or sets the search priority level.
        /// </summary>
        /// <value>
        /// The search priority level.
        /// </value>
        public int SearchPriorityLevel { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is finished.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is finished; otherwise, <c>false</c>.
        /// </value>
        [DefaultValue(false)]
        public bool IsFinished { get; set; }
        /// <summary>
        /// Gets or sets the searches.
        /// </summary>
        /// <value>
        /// The searches.
        /// </value>
        public List<SearchTransaction> Searches { get; set; }
    }
}
