using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NameSearch.Api.Controllers.Interfaces;
using NameSearch.Repository;
using NameSearch.Extensions;
using Serilog;
using NameSearch.Utility.Interfaces;
using System.Threading;
using AutoMapper;
using Newtonsoft.Json;

namespace NameSearch.App.Services
{
    /// <summary>
    /// Run Person Search
    /// </summary>
    public class PersonSearchRequestHelper
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger = Log.Logger.ForContext<PersonSearchRequestHelper>();
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IEntityFrameworkRepository Repository;
        /// <summary>
        /// The serializer settings
        /// </summary>
        private readonly JsonSerializerSettings SerializerSettings;
        /// <summary>
        /// The find person controller
        /// </summary>
        private readonly IFindPersonController FindPersonController;
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper Mapper;
        /// <summary>
        /// The export
        /// </summary>
        private readonly IExport Export;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonSearchRequestHelper"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="findPersonController">The find person controller.</param>
        /// <param name="export">The export.</param>
        public PersonSearchRequestHelper(IEntityFrameworkRepository repository,
            IFindPersonController findPersonController,
            JsonSerializerSettings serializerSettings,
            IMapper mapper,
            IExport export)
        {
            this.Repository = repository;
            this.SerializerSettings = serializerSettings;
            this.FindPersonController = findPersonController;
            this.Mapper = mapper;
            this.Export = export;
        }

        /// <summary>
        /// Searches the specified person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <param name="personSearchJobId">The person search job identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">person</exception>
        public async Task<Models.Entities.PersonSearchResult> SearchAsync(Models.Entities.PersonSearchRequest personSearchRequest, CancellationToken cancellationToken)
        {
            if (personSearchRequest == null)
            {
                throw new ArgumentNullException(nameof(personSearchRequest));
            }

            var log = logger.With("person", personSearchRequest);

            var stopwatch = new Stopwatch();

            try
            {
                stopwatch.Start();

                var person = Mapper.Map<Models.Domain.Api.Request.Person>(personSearchRequest);
                log.DebugEvent("Search", "Mapped PersonSearchRequest entity to Person domain model after {ms}ms", stopwatch.ElapsedMilliseconds);

                #region Execute Find Person request

                var result = await FindPersonController.GetFindPerson(person);

                log.With("Person", person)
                    .InformationEvent("Search", "Executed Find Person request after {ms}ms", stopwatch.ElapsedMilliseconds);

                var jObject = JObject.Parse(result.Content);

                log.ForContext("Content", jObject);

                #endregion

                #region Save Response to JSON text file

                await this.Export.ToJsonAsync(jObject, $"SearchJob-{personSearchRequest.Id}-{person.Name}", cancellationToken);

                #endregion

                #region Create PersonSearchResult Entity

                var personSearchResult = new Models.Entities.PersonSearchResult();
                personSearchResult.PersonSearchRequestId = personSearchRequest.Id;
                personSearchResult.HttpStatusCode = result.StatusCode;
                personSearchResult.NumberOfResults = (int)jObject["count_person"];
                personSearchResult.Warnings = (string)jObject["warnings"];
                personSearchResult.Error = (string)jObject["error"];
                personSearchResult.Data = jObject.ToString();

                log.With("PersonSearchResult", personSearchResult);

                #endregion

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

                #endregion

                #region Save Entity to Database

                Repository.Create(personSearchResult);
                await Repository.SaveAsync();

                #endregion

                #region Update PersonSearchRequest

                personSearchRequest.IsProcessed = true;
                Repository.Update(personSearchRequest);
                await Repository.SaveAsync();

                #endregion

                stopwatch.Stop();
                log.DebugEvent("Search", "Finished processing person search result after {ms}ms", stopwatch.ElapsedMilliseconds);

                return personSearchResult;
            }
            catch (Exception ex)
            {
                //Log and throw
                log.ErrorEvent(ex, "Search", "Processing person failed after {ms}ms", stopwatch.ElapsedMilliseconds);
                throw;
            }
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
