using System.Collections.Generic;
using System.ComponentModel;
using NameSearch.Models.Entities.Abstracts;

namespace NameSearch.Models.Entities
{
    /// <inheritdoc />
    public class SearchJob : EntityBase<SearchJob>
    {
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

        #region Equality

        public override bool Equals(SearchJob other)
        {
            if (other == null) return false;
            return SearchPriorityLevel == other.SearchPriorityLevel &&
                IsFinished == other.IsFinished &&
                Searches.Equals(other.Searches);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (this is null) return false;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as SearchJob);
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
