using System.Threading.Tasks;

namespace NameSearch.App.Tasks
{
    public class SearchName : ISearchName
    {
        public Task<bool> Import(string filePath)
        {
            return Task.Run(() => true);
        }

        public Task<bool> Export(string filePath)
        {
            return Task.Run(() => true);
        }
    }
}
