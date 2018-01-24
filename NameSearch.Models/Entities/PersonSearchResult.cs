using System.ComponentModel.DataAnnotations.Schema;
using NameSearch.Models.Entities.Abstracts;

namespace NameSearch.Models.Entities
{
    /// <summary>
    /// Search Transaction Entity
    /// </summary>
    /// <seealso cref="NameSearch.Models.Entities.Abstracts.EntityBase{NameSearch.Models.Entities.PersonSearchResult}" />
    /// <inheritdoc />
    public class PersonSearchResult : EntityBase<PersonSearchResult>
    {
        /// <summary>
        /// Gets or sets the search job identifier.
        /// </summary>
        /// <value>
        /// The search job identifier.
        /// </value>
        [ForeignKey("PersonSearchJobForeignKey")]
        public long PersonSearchJobId { get; set; }
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

        #region Equality

        /// <summary>
        /// See http://www.aaronstannard.com/overriding-equality-in-dotnet/
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(PersonSearchResult other)
        {
            if (other == null) return false;
            return PersonSearchJobId == other.PersonSearchJobId &&
                HttpStatusCode == other.HttpStatusCode &&
                NumberOfResults == other.NumberOfResults &&
                string.Equals(Warnings, other.Warnings) &&
                string.Equals(Error, other.Error) &&
                string.Equals(Data, other.Data);
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
            return Equals(obj as PersonSearchResult);
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
