using AutoMapper;
using NameSearch.Api.Controllers.Interfaces;
using NameSearch.App.Factories;
using NameSearch.App.Services;
using NameSearch.Extensions;
using NameSearch.Models.Domain;
using NameSearch.Models.Entities;
using NameSearch.Repository.Interfaces;
using NameSearch.Utility.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
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
        /// The repository
        /// </summary>
        private readonly IEntityFrameworkRepository Repository;

        /// <summary>
        /// The serializer settings
        /// </summary>
        private readonly JsonSerializerSettings SerializerSettings;

        private readonly string ResultOutputPath;

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
            IExport export,
            string resultOutputPath)
        {
            this.Repository = repository;
            this.SerializerSettings = serializerSettings;
            this.FindPersonController = findPersonController;
            this.Mapper = mapper;
            this.Export = export;
            this.ResultOutputPath = resultOutputPath;
        }

        /// <summary>
        /// Searches the specified person.
        /// </summary>
        /// <param name="serach">The search criteria.</param>
        /// <param name="searchWaitMs">The search wait in ms.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">person</exception>
        public async Task<PersonSearch> SearchAsync(Search search, int searchWaitMs, CancellationToken cancellationToken)
        {
            if (search == null)
            {
                throw new ArgumentNullException(nameof(search));
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

                var jObject = JObject.Parse(result.Content);

                log.ForContext("Content", jObject);

                #endregion Execute Find Person request

                #region Save Response to JSON text file

                var fullPath = Path.Combine(this.ResultOutputPath, $"SearchJob-{person.Name}");
                await this.Export.ToJsonAsync(jObject, fullPath, cancellationToken);

                #endregion Save Response to JSON text file

                #region Create PersonSearchResult Entity

                var personSearch = PersonSearchResultFactory.Create(search, result.StatusCode, jObject);

                log.With("PersonSearchResult", personSearch);

                #endregion Create PersonSearchResult Entity

                #region Log Data Problems

                if (!string.IsNullOrWhiteSpace(personSearch.Warnings))
                {
                    log.WarningEvent("Search", "FindPerson api result returned with warning messages");
                }
                if (!string.IsNullOrWhiteSpace(personSearch.Error))
                {
                    log.ErrorEvent("Search", "FindPerson api result returned with error message");
                }
                if (string.IsNullOrWhiteSpace(personSearch.Data))
                {
                    log.ErrorEvent("Search", "FindPerson api result returned with no person data"); ;
                }

                #endregion Log Data Problems

                //todo fix all of this
                #region Save Entity to Database

                Repository.Create(personSearch);
                Repository.Save();

                #endregion Save Entity to Database

                stopwatch.Stop();
                log.DebugEvent("Search", "Finished processing person search result after {ms}ms", stopwatch.ElapsedMilliseconds);

                return personSearch;
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