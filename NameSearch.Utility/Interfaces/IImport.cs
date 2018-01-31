using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NameSearch.Utility.Interfaces
{
    /// <summary>
    /// Interface for Import Utility
    /// </summary>
    public interface IImport
    {
        /// <summary>
        /// Froms the CSV.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        IEnumerable<T> FromCsv<T>(string fileName);

        /// <summary>
        /// Froms the json.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        JObject FromJson(string fileName);

        /// <summary>
        /// Froms the json asynchronous.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<JObject> FromJsonAsync(string fileName, CancellationToken cancellationToken);
    }
}