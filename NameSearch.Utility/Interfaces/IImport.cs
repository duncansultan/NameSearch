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
        /// <param name="fullPath">The full path.</param>
        /// <returns></returns>
        IEnumerable<T> FromCsv<T>(string fullPath);

        /// <summary>
        /// Froms the json.
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        /// <returns></returns>
        JObject FromJson(string fullPath);

        /// <summary>
        /// Froms the json asynchronous.
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<JObject> FromJsonAsync(string fullPath, CancellationToken cancellationToken);
    }
}