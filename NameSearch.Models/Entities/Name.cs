﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NameSearch.Models.Entities.Abstracts;

namespace NameSearch.Models.Entities
{
    /// <summary>
    /// SearchName Entity
    /// </summary>
    /// <seealso cref="NameSearch.Models.Entities.Abstracts.EntityBase{NameSearch.Models.Entities.Name}" />
    /// <inheritdoc />
    public class Name : EntityBase<Name>
    {
        /// <summary>
        /// Gets or sets the search name Group identifier.
        /// </summary>
        /// <value>
        /// The search name Group identifier.
        /// </value>
        [ForeignKey("NameImportForeignKey")]
        public long NameImportId { get; set; }
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
        /// Gets or sets the name import.
        /// </summary>
        /// <value>
        /// The name import.
        /// </value>
        [InverseProperty("Names")]
        public NameImport NameImport { get; set; }

        #region Equality

        /// <summary>
        /// See http://www.aaronstannard.com/overriding-equality-in-dotnet/
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(Name other)
        {
            if (other == null) return false;
            return NameImportId == other.NameImportId &&
                string.Equals(Value, other.Value) &&
                string.Equals(Description, other.Description) &&
                NameImport.Equals(other.NameImport);
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
            return Equals(obj as Name);
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
