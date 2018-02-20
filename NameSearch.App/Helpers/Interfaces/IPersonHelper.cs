using System.Collections.Generic;

namespace NameSearch.App.Helpers.Interfaces
{
    /// <summary>
    /// Interface for Person Helper
    /// </summary>
    public interface IPersonHelper
    {
        /// <summary>
        /// Gets the people.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Models.Domain.Person> GetPeople();
    }
}
