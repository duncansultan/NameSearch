﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NameSearch.Models.Entities.Abstracts;

namespace NameSearch.Models.Entities
{
    /// <inheritdoc />
    public class Person : EntityBase<Person>
    {
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

        #region Equality

        public override bool Equals(Person other)
        {
            if (other == null) return false;
            return string.Equals(FirstName, other.FirstName) &&
                string.Equals(LastName, other.LastName) &&
                string.Equals(Alias, other.Alias) &&
                Age == other.Age &&
                Addresses.Equals(other.Addresses);
        }

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
        /// <returns></returns>
        public override int GetHashCode() => base.GetHashCode();

        #endregion
    }
}
