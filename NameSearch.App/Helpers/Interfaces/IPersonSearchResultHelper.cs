using NameSearch.Models.Entities;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace NameSearch.App.Helpers.Interfaces
{
    /// <summary>
    /// Interface for Person Search Result Helper
    /// </summary>
    public interface IPersonSearchResultHelper
    {
        /// <summary>
        /// Imports the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="jObject">The j object.</param>
        /// <returns></returns>
        PersonSearch Import(string fileName, JObject jObject);

        /// <summary>
        /// Processes the specified person search.
        /// </summary>
        /// <param name="personSearch">The person search.</param>
        /// <returns></returns>
        IEnumerable<Person> Process(PersonSearch personSearch);
    }
}
