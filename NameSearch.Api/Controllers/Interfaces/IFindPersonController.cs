using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NameSearch.Api.Controllers.Interfaces
{
    public interface IFindPersonController
    {
        Task<JsonResult> GetPerson(IPerson model);
        Task<JsonResult> GetPerson(string name);
    }
}
