using NameSearch.Models.Domain;
using NameSearch.Models.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace NameSearch.App.Helpers.Interfaces
{
    /// <summary>
    /// Interface for Run Person Search
    /// </summary>
    public interface IPersonSearchRequestHelper
    {
        /// <summary>
        /// Searches the asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="resultOutputPath">The result output path.</param>
        /// <param name="searchWaitMs">The search wait ms.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<PersonSearch> SearchAsync(Search search, string resultOutputPath, int searchWaitMs, CancellationToken cancellationToken);
    }
}
