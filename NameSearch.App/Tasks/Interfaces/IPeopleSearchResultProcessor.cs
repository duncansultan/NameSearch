using NameSearch.Models.Entities;
using System.Threading.Tasks;

namespace NameSearch.App.Tasks
{
    public interface IPeopleSearchResultProcessor
    {
        /// <summary>
        /// Runs the specified person search job.
        /// </summary>
        /// <param name="personSearchJob">The person search job.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// personSearchJob
        /// or
        /// PersonSearchResults
        /// </exception>
        Task<bool> Run(PersonSearchJob personSearchJob);
    }
}
