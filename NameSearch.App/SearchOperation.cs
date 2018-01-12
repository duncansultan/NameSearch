using NameSearch.Repository;
using System.Threading.Tasks;
using NameSearch.Models.Domain;
using NameSearch.Models.Entities;
using NameSearch.Api.Controllers.Interfaces;
using AutoMapper;
using Newtonsoft.Json.Linq;
using Serilog;
using NameSearch.Utility;
using NameSearch.Utility.Interfaces;

namespace NameSearch.App
{
    public class SearchOperation
    {
        private readonly IEntityFrameworkRepository Repository;
        private readonly IFindPersonController FindPersonController;
        private readonly IMapper Mapper;
        private IExport Export;

        public SearchOperation(IEntityFrameworkRepository repository, 
            IFindPersonController findPersonController, 
            IMapper mapper,
            string outputDirectory)
        {
            this.Repository = repository;
            this.FindPersonController = findPersonController;
            this.Mapper = mapper;
            this.Export = new Export(outputDirectory);
        }

        public async Task LoadPeople(SearchPriorityTypes searchPriority, string state, string city = null, string zip = null)
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
                var model = new PersonSearch
                {
                    Name = name.Description,
                    City = city,
                    State = state,
                    Zip = zip
                };

                var result = await FindPersonController.GetPerson(model);
                var json = result.Value.ToString();

                //ToDo: Make this save process robust
                try
                {
                    this.Export.ToJson(json, $"SearchJob-{searchJob.Id}-{name.Description}");
                }
                catch (System.Exception ex)
                {
                    Log.Error(ex, "Failed to save FindPerson api result.");
                    throw;
                }
                
                JObject searchResult = JObject.Parse(json);
                string countPerson = (string) searchResult["count_person"];
                string warnings = (string)searchResult["warnings"];
                string error = (string)searchResult["error"];

                if (!string.IsNullOrWhiteSpace(warnings))
                {
                    Log.Error("FindPerson api result returned with warning messages.", warnings);
                }

                if (!string.IsNullOrWhiteSpace(error))
                {
                    Log.Error("FindPerson api result returned with error messages.", error);
                }

                int.TryParse(countPerson, out int numberOfResults);

                var search = new SearchTransaction
                {
                    SearchJobId = searchJob.Id,
                    NameId = name.Id,
                    StatusCode = result.StatusCode,
                    NumberOfResults = numberOfResults,
                    Json = json
                };                

                //ToDo: figure out how to bind child records do I need to use Repository.Create here?
                searchJob.Searches.Add(search);                
                Repository.Update(searchJob);
                Repository.Save();
            }

            searchJob.IsFinished = true;
            Repository.Update(searchJob);
            Repository.Save();
        }
    }
}
