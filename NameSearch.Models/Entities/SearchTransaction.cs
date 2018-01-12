using System.ComponentModel.DataAnnotations.Schema;
using NameSearch.Models.Entities.Abstracts;

namespace NameSearch.Models.Entities
{
    /// <summary>
    /// Search Transaction Entity
    /// </summary>
    /// <seealso cref="NameSearch.Models.Entities.Abstracts.EntityBase{NameSearch.Models.Entities.SearchTransaction}" />
    /// <inheritdoc />
    public class SearchTransaction : EntityBase<SearchTransaction>
    {
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
        /// Gets or sets the number of results.
        /// </summary>
        /// <value>
        /// The number of results.
        /// </value>
        public int NumberOfResults { get; set; }
        /// <summary>
        /// Gets or sets the json.
        /// </summary>
        /// <value>
        /// The json.
        /// </value>
        public string Json { get; set; }

        #region Equality

        /// <summary>
        /// See http://www.aaronstannard.com/overriding-equality-in-dotnet/
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(SearchTransaction other)
        {
            if (other == null) return false;
            return SearchJobId == other.SearchJobId &&
                NameId == other.NameId &&
                StatusCode == other.StatusCode &&
                NumberOfResults == other.NumberOfResults &&
                string.Equals(Json, other.Json);
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
            return Equals(obj as SearchTransaction);
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
