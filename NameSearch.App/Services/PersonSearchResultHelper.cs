using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NameSearch.Repository;
using NameSearch.Extensions;
using Serilog;
using System.Threading;
using AutoMapper;
using Newtonsoft.Json;

namespace NameSearch.App.Services
{
    /// <summary>
    /// Run Person Search
    /// </summary>
    public class PersonSearchResultHelper
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger = Log.Logger.ForContext<PersonSearchResultHelper>();
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IEntityFrameworkRepository Repository;
        /// <summary>
        /// The serializer settings
        /// </summary>
        private readonly JsonSerializerSettings SerializerSettings;
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper Mapper;

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
        /// Processes the specified person search result.
        /// </summary>
        /// <param name="personSearchResult">The person search result.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">personSearchResult</exception>
        public async Task<Models.Entities.Person> ProcessAsync(Models.Entities.PersonSearchResult personSearchResult, CancellationToken cancellationToken)
        {
            if (personSearchResult == null)
            {
                throw new ArgumentNullException(nameof(personSearchResult));
            }

            var log = logger.With("personSearchResult", personSearchResult);

            var stopwatch = new Stopwatch();

            #region Deserialize JSON into Model

            Models.Domain.Api.Response.IFindPersonResponse findPersonResponse;

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

            #endregion

            #region Map Model into Entity

            var personEntity = Mapper.Map<Models.Entities.Person>(findPersonResponse.Person);

            log.With("Person", personEntity);

            #endregion

            #region Save Entity to Database

            Repository.Create(personEntity);
            await Repository.SaveAsync();

            log.With("Data", personSearchResult.Data)
                .InformationEvent("Run", "Created Person record after {ms}ms", stopwatch.ElapsedMilliseconds);

            #endregion

            log.InformationEvent("Run", "Processing search result finished after {ms}ms", stopwatch.ElapsedMilliseconds);

            return personEntity;
        }
    }
}
