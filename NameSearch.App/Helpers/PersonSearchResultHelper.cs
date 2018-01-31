using AutoMapper;
using NameSearch.App.Factories;
using NameSearch.Extensions;
using NameSearch.Models.Domain.Api.Response.Interfaces;
using NameSearch.Models.Entities;
using NameSearch.Repository;
using NameSearch.Repository.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace NameSearch.App.Helpers
{
    /// <summary>
    /// Run Person Search
    /// </summary>
    public class PersonSearchResultHelper
    {
        #region Dependencies

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger = Log.Logger.ForContext<PersonSearchResultHelper>();

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper Mapper;

        /// <summary>
        /// The repository
        /// </summary>
        private readonly IEntityFrameworkRepository Repository;

        /// <summary>
        /// The serializer settings
        /// </summary>
        private readonly JsonSerializerSettings SerializerSettings;
        #endregion Dependencies

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonSearchResultHelper"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="serializerSettings">The serializer settings.</param>
        /// <param name="mapper">The mapper.</param>
        public PersonSearchResultHelper(IEntityFrameworkRepository repository,
            JsonSerializerSettings serializerSettings,
            IMapper mapper)
        {
            this.Repository = repository;
            this.SerializerSettings = serializerSettings;
            this.Mapper = mapper;
        }

        /// <summary>
        /// Imports the asynchronous.
        /// </summary>
        /// <param name="jObject">The j object.</param>
        /// <param name="personSearchJobId">The person search job identifier.</param>
        /// <returns></returns>
        public async Task<PersonSearchResult> ImportAsync(JObject jObject, long personSearchJobId)
        {
            var log = logger.ForContext("personSearchJobId", personSearchJobId);

            #region Create Search Request Entity

            var personSearchRequest = new PersonSearchRequest
            {
                PersonSearchJobId = personSearchJobId
            };
            Repository.Create(personSearchRequest);
            await Repository.SaveAsync();

            #endregion Create Search Request Entity

            #region Create PersonSearchResult Entity

            var personSearchResult = PersonSearchResultFactory.Create(personSearchRequest.Id, null, jObject);

            log.With("PersonSearchResult", personSearchResult);

            #endregion Create PersonSearchResult Entity

            #region Log Data Problems

            if (!string.IsNullOrWhiteSpace(personSearchResult.Warnings))
            {
                log.WarningEvent("Search", "FindPerson api result returned with warning messages");
            }
            if (!string.IsNullOrWhiteSpace(personSearchResult.Error))
            {
                log.ErrorEvent("Search", "FindPerson api result returned with error message");
            }
            if (string.IsNullOrWhiteSpace(personSearchResult.Data))
            {
                log.ErrorEvent("Search", "FindPerson api result returned with no person data"); ;
            }

            #endregion Log Data Problems

            #region Save Entity to Database

            Repository.Create(personSearchResult);
            await Repository.SaveAsync();

            personSearchRequest.IsProcessed = true;
            Repository.Update(personSearchRequest);
            await Repository.SaveAsync();

            #endregion Save Entity to Database

            return personSearchResult;
        }

        /// <summary>
        /// Processes the specified person search result.
        /// </summary>
        /// <param name="personSearchResult">The person search result.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">personSearchResult</exception>
        public async Task<IEnumerable<Person>> ProcessAsync(PersonSearchResult personSearchResult, CancellationToken cancellationToken)
        {
            if (personSearchResult == null)
            {
                throw new ArgumentNullException(nameof(personSearchResult));
            }

            var log = logger.With("personSearchResult", personSearchResult);

            var stopwatch = new Stopwatch();

            #region Deserialize JSON into Model

            IFindPersonResponse findPersonResponse;

            try
            {
                findPersonResponse = JsonConvert.DeserializeObject<Models.Domain.Api.Response.FindPersonResponse>(personSearchResult.Data, SerializerSettings);
            }
            catch (JsonException ex)
            {
                //Log and throw
                log.With("Data", personSearchResult.Data)
                    .ErrorEvent(ex, "Run", "Json Data Deserialization failed after {ms}ms", stopwatch.ElapsedMilliseconds);
                throw;
            }

            #endregion Deserialize JSON into Model

            var people = new List<Person>();

            foreach (var person in findPersonResponse.Person)
            {
                #region Map Model into Entity

                var personEntity = Mapper.Map<Person>(person);
                people.Add(personEntity);

                log.With("Person", personEntity);

                #endregion Map Model into Entity

                #region Save Entity to Database

                Repository.Create(personEntity);
                await Repository.SaveAsync();

                log.With("Data", personSearchResult.Data)
                    .InformationEvent("Run", "Created Person record after {ms}ms", stopwatch.ElapsedMilliseconds);

                #endregion Save Entity to Database
            }

            log.InformationEvent("Run", "Processing search result finished after {ms}ms", stopwatch.ElapsedMilliseconds);

            return people;
        }
    }
}