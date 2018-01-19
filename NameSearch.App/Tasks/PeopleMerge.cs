using AutoMapper;
using NameSearch.Models.Entities;
using NameSearch.Repository;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NameSearch.App.Tasks
{
    public class PeopleMerge
    {
        private readonly IEntityFrameworkRepository Repository;
        private readonly IMapper Mapper;
        private readonly JsonSerializerSettings SerializerSettings;

        public PeopleMerge(IEntityFrameworkRepository repository, IMapper mapper, JsonSerializerSettings serializerSettings)
        {
            this.Repository = repository;
            this.Mapper = mapper;
            this.SerializerSettings = serializerSettings;
        }

        public async Task<bool> Run(SearchJob searchJob)
        {
            if (searchJob == null)
            {
                throw new ArgumentNullException(nameof(searchJob));
            }

            if (searchJob.IsFinished)
            {
                throw new ArgumentException($"SearchJob {searchJob.Id} is already processed.");
            }

            if (!searchJob.Searches.Any())
            {
                throw new ArgumentNullException("searchJob.Searches", $"SearchJob {searchJob.Id} has no Searches.");
            }

            int newRecords = 0;
            foreach (var search in searchJob.Searches)
            {
                //ToDo: Fix the Deserializer
                var personSearchResult = JsonConvert.DeserializeObject<Person>(search.Data);
                var person = Mapper.Map<Person>(personSearchResult);

                var exists = Repository.GetExists<Person>(x => x.Equals(person));
                if (!exists)
                {
                    Repository.Create(person);
                    newRecords++;
                }
                await Repository.SaveAsync();
            }



            searchJob.IsFinished = true;
            await Repository.SaveAsync();
            Log.Information($"{newRecords} processed for {searchJob.Id}.");

            return true;
        }
    }
}
