using System.Threading.Tasks;
using NameSearch.Models.Domain.Api.Response;

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
        Task<IApiResponse> GetFindPerson(Models.Domain.Api.Request.IPerson model);
    }
}
