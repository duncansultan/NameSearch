using AutoMapper;
using NameSearch.Api.Controllers.Interfaces;
using NameSearch.App.Builders;
using NameSearch.App.Factories;
using NameSearch.Extensions;
using NameSearch.Models.Domain;
using NameSearch.Models.Entities;
using NameSearch.Repository.Interfaces;
using NameSearch.Utility.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace NameSearch.App.Helpers
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
        /// The person search result builder
        /// </summary>
        private readonly PersonSearchResultBuilder PersonSearchResultBuilder;

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
            this.PersonSearchResultBuilder = new PersonSearchResultBuilder(serializerSettings);
        }

        /// <summary>
        /// Searches the specified person.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="resultOutputPath">The result output path.</param>
        /// <param name="searchWaitMs">The search wait in ms.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">person</exception>
        public async Task<PersonSearch> SearchAsync(Search search, string resultOutputPath, int searchWaitMs, CancellationToken cancellationToken)
        {
            if (search == null)
            {
                throw new ArgumentNullException(nameof(search));
            }
            if (string.IsNullOrWhiteSpace(search.Name))
            {
                throw new ArgumentNullException(nameof(search.Name));
            }

            var log = logger.With("search", search);

            var stopwatch = new Stopwatch();

            try
            {
                stopwatch.Start();

                var person = Mapper.Map<Models.Domain.Api.Request.Person>(search);
                log.DebugEvent("Search", "Mapped PersonSearchRequest entity to Person domain model after {ms}ms", stopwatch.ElapsedMilliseconds);

                #region Execute Find Person request

                var result = await FindPersonController.GetFindPerson(person);

                await Task.Delay(searchWaitMs);

                log.With("Person", person)
                    .InformationEvent("Search", "Executed Find Person request after {ms}ms", stopwatch.ElapsedMilliseconds);

                var jContent = JObject.Parse(result.Content);

                log.ForContext("Content", jContent);

                #endregion Execute Find Person request

                #region Get JObject for JSON Export

                var jExport = SearchResponseJObjectFactory.Get(search, jContent);
                log.DebugEvent("Search", "Mapped Search domain model and response JObject to new JObject for JSON Export after {ms}ms", stopwatch.ElapsedMilliseconds);

                #endregion Get JObject for JSON Export

                #region Save Response to JSON text file

                var fileName = GetResponseFileName(search);
                var fullPath = Path.Combine(resultOutputPath, fileName);
                await this.Export.ToJsonAsync(jExport, fullPath, cancellationToken);

                #endregion Save Response to JSON text file

                #region Create PersonSearchResult Entity

                var personSearchResult = PersonSearchResultBuilder.Create(search, result.StatusCode, jContent);

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
                Repository.Save();

                #endregion Save Entity to Database

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
        /// Gets the name of the response file.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        private static string GetResponseFileName(Search search)
        {
            string fileName = string.Empty;
            if (!string.IsNullOrWhiteSpace(search.Criteria.State))
            {
                fileName += $"State-{search.Criteria.State}_";
            }
            if (!string.IsNullOrWhiteSpace(search.Criteria.City))
            {
                fileName += $"City-{search.Criteria.City}_";
            }
            if (!string.IsNullOrWhiteSpace(search.Criteria.Zip))
            {
                fileName += $"Zip-{search.Criteria.Zip}_";
            }
            if (!string.IsNullOrWhiteSpace(search.Criteria.Address1))
            {
                fileName += $"Address1-{search.Criteria.Address1}_";
            }
            if (!string.IsNullOrWhiteSpace(search.Criteria.Address2))
            {
                fileName += $"Address2-{search.Criteria.Address2}_";
            }
            fileName += $"Name-{search.Name}.json";

            return fileName;
        }
    }
}