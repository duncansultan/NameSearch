using System.Threading.Tasks;

namespace NameSearch.App.Tasks
{
    public interface ISearchNameImporter
    {
        Task<bool> Run();
    }
}
