﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using NameSearch.Models.Entities.Abstracts;
using NameSearch.Models.Entities.Interfaces;

namespace NameSearch.Models.Entities
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:NameSearch.Models.Entities.Interfaces.IEntity" />
    public class SearchTransaction : AuditableEntityBase, IEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }
        /// <summary>
        /// Gets or sets the search job identifier.
        /// </summary>
        /// <value>
        /// The search job identifier.
        /// </value>
        [ForeignKey("SearchJobForeignKey")]
        public long SearchJobId { get; set; }
        /// <summary>
        /// Gets or sets the name identifier.
        /// </summary>
        /// <value>
        /// The name identifier.
        /// </value>
        [ForeignKey("NameForeignKey")]
        public long NameId { get; set; }
        /// <summary>
        /// Gets or sets the HTTP response.
        /// </summary>
        /// <value>
        /// The HTTP response.
        /// </value>
        public int? StatusCode { get; set; }
        /// <summary>
        /// Gets or sets the json.
        /// </summary>
        /// <value>
        /// The json.
        /// </value>
        public string Json { get; set; }
    }
}
