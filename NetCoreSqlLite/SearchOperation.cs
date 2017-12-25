using NameSearch.Repository;
using System.Threading.Tasks;
using NameSearch.Models.Domain;
using NameSearch.Models.Entities;
using NameSearch.Api.Controllers.Interfaces;

namespace NameSearch.App
{
    public class SearchOperation
    {
        private readonly IEntityFrameworkRepository Repository;
        private readonly IFindPersonController FindPersonController;

        public SearchOperation(IEntityFrameworkRepository repository, IFindPersonController findPersonController)
        {
            this.Repository = repository;
            this.FindPersonController = findPersonController;
        }

        public async Task LoadPeople(SearchPriorityTypes searchPriority)
        {
            var names = Repository.Get<Name>(x => x.IsActive && x.SearchPriorityLevel == (int)searchPriority);

            var searchJob = new SearchJob
            {
                SearchPriorityLevel = (int)searchPriority
            };
            Repository.Create(searchJob);
            Repository.Save();

            foreach (var name in names)
            {
                var result = await FindPersonController.GetPerson(name.Description);

                var search = new SearchTransaction
                {
                    NameId = name.Id,
                    StatusCode = result.StatusCode,
                    Json = result.ToString()
                };
                
                searchJob.Searches.Add(search);
                //ToDo: figure out how to bind child records
                //Or do I need to use Repository.Create here?
                //Repository.Update(searchJob);
                //Repository.Save();
            }

            searchJob.IsFinished = true;
            Repository.Update(searchJob);
            Repository.Save();
        }
    }
}
