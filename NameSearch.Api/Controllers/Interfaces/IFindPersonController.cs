using System.Threading.Tasks;

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
        ///     Api Response
        /// </returns>
        Task<Models.Domain.Api.Response.IApiResponse> GetFindPerson(Models.Domain.Api.Request.IPerson model);
    }
}
