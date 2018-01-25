using AutoMapper;
using NameSearch.Models.Entities;
using NameSearch.Repository;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Diagnostics;
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
        /// The logger
        /// </summary>
        private ILogger logger = Log.Logger.ForContext<PeopleSearchResultProcessor>();
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

            var stopwatch = new Stopwatch();

            //Start
            stopwatch.Start();

            foreach (var personSearchResult in personSearchJob.PersonSearchResults)
            {
                Models.Domain.Api.Response.Person person;

                try
                {
                    //ToDo: Create Custom Deserializer setting
                    person = JsonConvert.DeserializeObject<Models.Domain.Api.Response.Person>(personSearchResult.Data, SerializerSettings);
                }
                catch (JsonException ex)
                {
                    logger.ForContext("Data", personSearchResult.Data)
                        .ForContext("PersonSearchJob.Id", personSearchJob.Id)
                        .Warning(ex, "<{EventID:l}> - {message} - {ms} ms", "Run", "Json Data Deserialization Failed", stopwatch.ElapsedMilliseconds);

                    continue;
                }
                
                var personEntity = Mapper.Map<Models.Entities.Person>(person);
                personEntity.PersonSearchResultId = personSearchResult.Id;

                Repository.Create(personEntity);
                await Repository.SaveAsync();

                logger.ForContext("Person", person)
                   .ForContext("PersonSearchJob.Id", personSearchJob.Id)
                   .Information("<{EventID:l}> - {message} - {ms} ms", "Run", "Created Person Record", stopwatch.ElapsedMilliseconds);
            }

            personSearchJob.IsProcessed = true;
            Repository.Update(personSearchJob);
            await Repository.SaveAsync();

            stopwatch.Stop();

            logger.ForContext("PersonSearchJob.Id", personSearchJob.Id)
                .Information("<{EventID:l}> - {message} - {ms} ms", "Run", "Processing finished", stopwatch.ElapsedMilliseconds);

            return personSearchJob.IsProcessed;
        }
    }
}
