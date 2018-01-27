using System.Threading.Tasks;

namespace NameSearch.App.Tasks
{
    public interface ISearchName
    {
        Task<bool> Import(string filePath);

        Task<bool> Export(string filePath);
    }
}
