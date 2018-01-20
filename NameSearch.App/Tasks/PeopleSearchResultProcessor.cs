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
    public class PeopleSearchResultProcessor
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IEntityFrameworkRepository Repository;
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper Mapper;
        /// <summary>
        /// The serializer settings
        /// </summary>
        private readonly JsonSerializerSettings SerializerSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeopleSearchResultProcessor"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="serializerSettings">The serializer settings.</param>
        public PeopleSearchResultProcessor(IEntityFrameworkRepository repository, 
            IMapper mapper, 
            JsonSerializerSettings serializerSettings)
        {
            this.Repository = repository;
            this.Mapper = mapper;
            this.SerializerSettings = serializerSettings;
        }

        public async Task<bool> Run(PersonSearchJob searchJob)
        {
            if (searchJob == null)
            {
                throw new ArgumentNullException(nameof(searchJob));
            }
            if (!searchJob.PersonSearchResuts.Any())
            {
                throw new ArgumentNullException("searchJob.Searches", $"SearchJob {searchJob.Id} has no Searches.");
            }
            if (searchJob.IsFinished)
            {
                throw new ArgumentException($"SearchJob {searchJob.Id} is already processed.");
            }

            int newRecords = 0;
            foreach (var search in searchJob.PersonSearchResuts)
            {
                //ToDo: Fix the Deserializer
                var personSearchResult = JsonConvert.DeserializeObject<Models.Domain.Api.Response.Person>(search.Data);
                var person = Mapper.Map<Models.Entities.Person>(personSearchResult);

                var exists = Repository.GetExists<Models.Entities.Person>(x => x.Equals(person));
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
