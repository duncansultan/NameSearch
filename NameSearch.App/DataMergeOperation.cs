using AutoMapper;
using NameSearch.Models.Entities;
using NameSearch.Repository;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Linq;

namespace NameSearch.App
{
    public class DataMergeOperation
    {
        private readonly IEntityFrameworkRepository Repository;
        private readonly IMapper Mapper;
        private readonly JsonSerializerSettings SerializerSettings;

        public DataMergeOperation(IEntityFrameworkRepository repository, IMapper mapper, JsonSerializerSettings serializerSettings)
        {
            this.Repository = repository;
            this.Mapper = mapper;
            this.SerializerSettings = serializerSettings;
        }

        public void MergePeople(long searchJobId)
        {
            var searchJob = Repository.GetFirst<SearchJob>(x => x.Id == searchJobId, null, "Searches");

            this.MergePeople(searchJob.Id);
        }

        public void MergePeople(SearchJob searchJob)
        {
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
                var personDomainModel = JsonConvert.DeserializeObject<Models.Domain.Person>(search.Json);
                var personEntity = Mapper.Map<Person>(personDomainModel);

                var exists = Repository.GetExists<Person>(x => x.Equals(personEntity));
                if (!exists)
                {
                    Repository.Create(personEntity);
                    newRecords++;
                }
                Repository.Save();
            }
            searchJob.IsFinished = true;
            Repository.Save();
            Log.Information($"{newRecords} processed for {searchJob.Id}.");
        }
    }
}
