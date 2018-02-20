using NameSearch.Models.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NameSearch.App.Services
{
    /// <summary>
    /// Interface for Person Search Service
    /// </summary>
    public interface IPeopleSearchService
    {
        /// <summary>
        /// Exports to CSV.
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        void ExportToCsv(string fullPath);
        /// <summary>
        /// Imports the searches.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        void ImportSearches(string folderPath);
        /// <summary>
        /// Processes the results.
        /// </summary>
        /// <param name="reprocess">if set to <c>true</c> [reprocess].</param>
        void ProcessResults(bool reprocess = false);
        /// <summary>
        /// Searches the asynchronous.
        /// </summary>
        /// <param name="searches">The searches.</param>
        /// <param name="resultOutputPath">The result output path.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool> SearchAsync(IEnumerable<Search> searches, string resultOutputPath, CancellationToken cancellationToken);
    }
}
