using Newtonsoft.Json.Linq;
using System.Collections;

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
        /// <param name="fileName">Name of the file.</param>
        /// <param name="isAppend">if set to <c>true</c> [is append].</param>
        void ToCsv<T>(T records, string fileName, bool isAppend) where T : IEnumerable;
        /// <summary>
        /// To the json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <param name="fileName">Name of the file.</param>
        void ToJson(string json, string fileName);
        /// <summary>
        /// To the json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <param name="fileName">Name of the file.</param>
        void ToJson(JObject json, string fileName);
    }
}
