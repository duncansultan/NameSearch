using AutoMapper;
using NameSearch.Models.Entities;
using NameSearch.Repository;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NameSearch.App.Tasks
{
    /// <summary>
    /// Process Search Results
    /// </summary>
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

        /// <summary>
        /// Runs the specified person search job.
        /// </summary>
        /// <param name="personSearchJob">The person search job.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// personSearchJob
        /// or
        /// PersonSearchResults
        /// </exception>
        public async Task<bool> Run(PersonSearchJob personSearchJob)
        {
            if (personSearchJob == null)
            {
                throw new ArgumentNullException(nameof(personSearchJob));
            }

            if (!personSearchJob.PersonSearchResults.Any())
            {
                throw new ArgumentNullException(nameof(personSearchJob.PersonSearchResults));
            }
            
            foreach (var personSearchResult in personSearchJob.PersonSearchResults)
            {
                var personResult = JsonConvert.DeserializeObject<Models.Domain.Api.Response.Person>(personSearchResult.Data);

                var personEntity = Mapper.Map<Models.Entities.Person>(personSearchResult);
                personEntity.PersonSearchJobId = personSearchJob.Id;
                personEntity.PersonSearchResultId = personSearchResult.Id;

                Repository.Create(personEntity);

                await Repository.SaveAsync();
            }

            personSearchJob.IsProcessed = true;

            await Repository.SaveAsync();

            return personSearchJob.IsProcessed;
        }
    }
}
