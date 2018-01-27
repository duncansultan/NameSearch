using System.Threading.Tasks;

namespace NameSearch.App.Tasks
{
    public class SearchNameImporter : ISearchNameImporter
    {
        public Task<bool> Run()
        {
            return Task.Run(() => true);
        }
    }
}
