using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NameSearch.Models.Entities.Abstracts;

namespace NameSearch.Models.Entities
{
    /// <summary>
    /// Search Transaction Entity
    /// </summary>
    /// <seealso cref="NameSearch.Models.Entities.Abstracts.EntityBase{NameSearch.Models.Entities.PersonSearch}" />
    /// <inheritdoc />
    public class PersonSearch : EntityBase<PersonSearch>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the address1.
        /// </summary>
        /// <value>
        /// The address1.
        /// </value>
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
        public string City { get; set; }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State { get; set; }
        /// <summary>
        /// Gets or sets the zip.
        /// </summary>
        /// <value>
        /// The zip.
        /// </value>
        public string Zip { get; set; }
        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is processed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is processed; otherwise, <c>false</c>.
        /// </value>
        public bool IsProcessed { get; set; }
        /// <summary>
        /// Gets or sets the HTTP response.
        /// </summary>
        /// <value>
        /// The HTTP response.
        /// </value>
        public int? HttpStatusCode { get; set; }
        /// <summary>
        /// Gets or sets the number of results.
        /// </summary>
        /// <value>
        /// The number of results.
        /// </value>
        public int NumberOfResults { get; set; }
        /// <summary>
        /// Gets or sets the warnings.
        /// </summary>
        /// <value>
        /// The warnings.
        /// </value>
        public string Warnings { get; set; }
        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public string Error { get; set; }
        /// <summary>
        /// Gets or sets the json data.
        /// </summary>
        /// <value>
        /// The json data.
        /// </value>
        public string Data { get; set; }
        /// <summary>
        /// Gets or sets the people.
        /// </summary>
        /// <value>
        /// The people.
        /// </value>
        public List<Person> People { get; set; }

        #region Equality

        /// <summary>
        /// See http://www.aaronstannard.com/overriding-equality-in-dotnet/
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(PersonSearch other)
        {
            if (other == null) return false;
            return string.Equals(Name, other.Name, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(Address1, other.Address1, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(Address2, other.Address2, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(State, other.State, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(Zip, other.Zip, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(Country, other.Country, StringComparison.CurrentCultureIgnoreCase) &&
                IsProcessed == other.IsProcessed &&
                int.Equals(HttpStatusCode, other.HttpStatusCode) &&
                NumberOfResults == other.NumberOfResults &&
                string.Equals(Warnings, other.Warnings, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(Error, other.Error, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(Data, other.Data, StringComparison.InvariantCultureIgnoreCase) &&
                (People ?? new List<Person>()).Equals(other.People);
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
            return Equals(obj as PersonSearch);
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
