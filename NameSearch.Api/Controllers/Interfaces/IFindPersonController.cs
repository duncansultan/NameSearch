using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using NameSearch.Api.Domain.Request;

namespace NameSearch.Api.Controllers.Interfaces
{
    /// <summary>
    /// Interface for Controller that Executes Requests to WhitePages Premium Find Person Api
    /// </summary>
    public interface IFindPersonController
    {
        /// <summary>
        /// Gets the person.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// JSON Result
        /// </returns>
        Task<JsonResult> GetPerson(IPersonSearch model);
    }
}
