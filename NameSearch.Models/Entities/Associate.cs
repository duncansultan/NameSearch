﻿using NameSearch.Models.Entities.Abstracts;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NameSearch.Models.Entities
{
    /// <summary>
    /// Associate Entity
    /// </summary>
    /// <seealso cref="NameSearch.Models.Entities.Abstracts.EntityBase{NameSearch.Models.Entities.Associate}" />
    /// <inheritdoc />
    public class Associate : EntityBase<Associate>
    {
        /// <summary>
        /// Gets or sets the external identifier.
        /// </summary>
        /// <value>
        /// The external identifier.
        /// </value>
        public string ExternalId { get; set; }
        /// <summary>
        /// Gets or sets the Full name of the associated person.
        /// </summary>
        /// <value>
        /// The Full name of the associated person.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the Relation of the associated person to the found person.
        /// </summary>
        /// <value>
        /// The Relation of the associated person to the found person.
        /// </value>
        public string Relation { get; set; }
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
        public override bool Equals(Associate other)
        {
            if (other == null) return false;
            return string.Equals(ExternalId, other.ExternalId, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(Name, other.Name, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(Relation, other.Relation, StringComparison.InvariantCultureIgnoreCase) &&
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
