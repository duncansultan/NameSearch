using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Threading;
using NameSearch.Models.Utility;

namespace NameSearch.App.Tasks
{
    /// <summary>
    /// Run Searches to Find People
    /// </summary>
    public interface IPeopleFinder
    {
        /// <summary>
        /// Runs the specified people.
        /// </summary>
        /// <param name="people">The people.</param>
        /// <returns></returns>
        Task<bool> Run<TProgressReport>(IEnumerable<Models.Domain.Api.Request.IPerson> people, IProgress<TProgressReport> progress, CancellationToken cancellationToken) where TProgressReport : IProgressReport, new();
    }
}
