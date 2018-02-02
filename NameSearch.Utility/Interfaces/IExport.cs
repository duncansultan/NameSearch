using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NameSearch.Utility.Interfaces
{
    /// <summary>
    /// Interface for Export Utility
    /// </summary>
    public interface IExport
    {
        /// <summary>
        /// To the CSV.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records">The records.</param>
        /// <param name="fullPath">The full path.</param>
        /// <param name="isAppend">if set to <c>true</c> [is append].</param>
        void ToCsv<T>(IEnumerable<T> records, string fullPath, bool isAppend);

        /// <summary>
        /// To the json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <param name="fullPath">The full path.</param>
        void ToJson(JObject json, string fullPath);

        /// <summary>
        /// To the json asynchronous.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <param name="fullPath">The full path.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task ToJsonAsync(JObject json, string fullPath, CancellationToken cancellationToken);

        /// <summary>
        /// To the txt.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="fullPath">The full path.</param>
        void ToTxt(string text, string fullPath);

        /// <summary>
        /// To the txt asynchronous.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="fullPath">The full path.</param>
        /// <returns></returns>
        Task ToTxtAsync(string text, string fullPath);
    }
}