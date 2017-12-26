using System.ComponentModel.DataAnnotations.Schema;
using NameSearch.Models.Entities.Abstracts;

namespace NameSearch.Models.Entities
{
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
        /// Gets or sets the json.
        /// </summary>
        /// <value>
        /// The json.
        /// </value>
        public string Json { get; set; }

        #region Equality

        public override bool Equals(SearchTransaction other)
        {
            if (other == null) return false;
            return SearchJobId == other.SearchJobId &&
                NameId == other.NameId &&
                StatusCode == other.StatusCode &&
                string.Equals(Json, other.Json);
        }

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
        /// <returns></returns>
        public override int GetHashCode() => base.GetHashCode();

        #endregion
    }
}
