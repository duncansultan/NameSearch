using AutoMapper;
using NameSearch.Api.Controllers.Interfaces;
using NameSearch.App.Factories;
using NameSearch.Extensions;
using NameSearch.Models.Entities;
using NameSearch.Repository;
using NameSearch.Utility.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace NameSearch.App.Services
{
    /// <summary>
    /// Run Person Search
    /// </summary>
    public class PersonSearchRequestHelper
    {
        #region Dependencies

        /// <summary>
        /// The export
        /// </summary>
        private readonly IExport Export;

        /// <summary>
        /// The find person controller
        /// </summary>
        private readonly IFindPersonController FindPersonController;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger = Log.Logger.ForContext<PersonSearchRequestHelper>();

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
        /// Gets the person search requests.
        /// </summary>
        /// <param name="personSearchJobId">The person search job identifier.</param>
        /// <returns></returns>
        public IEnumerable<PersonSearchRequest> Get(long personSearchJobId)
        {
            var personSearchRequests = Repository.Get<PersonSearchRequest>(x => x.PersonSearchJobId == personSearchJobId && !x.IsProcessed);
            return personSearchRequests;
        }

        /// <summary>
        /// Searches the specified person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <param name="personSearchJobId">The person search job identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">person</exception>
        public async Task<PersonSearchResult> SearchAsync(PersonSearchRequest personSearchRequest, CancellationToken cancellationToken)
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

                #endregion Execute Find Person request

                #region Save Response to JSON text file

                await this.Export.ToJsonAsync(jObject, $"SearchJob-{personSearchRequest.Id}-{person.Name}", cancellationToken);

                #endregion Save Response to JSON text file

                //todo

                #region Create PersonSearchResult Entity

                var personSearchResult = PersonSearchResultFactory.Create(personSearchRequest.Id, result.StatusCode, jObject);

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

                #endregion Save Entity to Database

                #region Update PersonSearchRequest

                personSearchRequest.IsProcessed = true;
                Repository.Update(personSearchRequest);
                await Repository.SaveAsync();

                #endregion Update PersonSearchRequest

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
    }
}