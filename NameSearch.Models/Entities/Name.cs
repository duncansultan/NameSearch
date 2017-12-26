using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NameSearch.Models.Entities.Abstracts;

namespace NameSearch.Models.Entities
{
    /// <inheritdoc />
    public class Name : EntityBase<Name>
    {
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
        /// Gets or sets the search priority level.
        /// </summary>
        /// <value>
        /// The search priority level.
        /// </value>
        [Range(1, 3)]
        public int SearchPriorityLevel { get; set; }
        /// <summary>
        /// Gets or sets the searches.
        /// </summary>
        /// <value>
        /// The searches.
        /// </value>
        public List<SearchTransaction> Searches { get; set; }

        #region Equality

        public override bool Equals(Name other)
        {
            if (other == null) return false;
            return string.Equals(Value, other.Value) &&
                string.Equals(Description, other.Description) &&
                SearchPriorityLevel == other.SearchPriorityLevel &&
                Searches.Equals(other.Searches);
        }

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
        /// <returns></returns>
        public override int GetHashCode() => base.GetHashCode();

        #endregion
    }
}
