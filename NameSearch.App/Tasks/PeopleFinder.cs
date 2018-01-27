using NameSearch.Repository;
using System.Threading.Tasks;
using NameSearch.Api.Controllers.Interfaces;
using Newtonsoft.Json.Linq;
using Serilog;
using NameSearch.Utility.Interfaces;
using System.Collections.Generic;
using System;
using NameSearch.Models.Entities;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using NameSearch.Extensions;

namespace NameSearch.App.Tasks
{
    /// <summary>
    /// Run Searches to Find People
    /// </summary>
    public class PeopleFinder
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger = Log.Logger.ForContext<PeopleFinder>();
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IEntityFrameworkRepository Repository;
        /// <summary>
        /// The find person controller
        /// </summary>
        private readonly IFindPersonController FindPersonController;
        /// <summary>
        /// The export
        /// </summary>
        private readonly IExport Export;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeopleFinder"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="findPersonController">The find person controller.</param>
        /// <param name="export">The export.</param>
        public PeopleFinder(IEntityFrameworkRepository repository,
            IFindPersonController findPersonController,
            IExport export)
        {
            this.Repository = repository;
            this.FindPersonController = findPersonController;
            this.Export = export;
        }

        /// <summary>
        /// Runs the specified people.
        /// </summary>
        /// <param name="people">The people.</param>
        /// <returns></returns>
        public async Task<bool> Run(IEnumerable<Models.Domain.Api.Request.IPerson> people, IProgress<Models.Domain.Api.Request.IPerson> progress, CancellationToken cancellationToken)
        {
            if (people == null || !people.Any())
            {
                throw new ArgumentNullException(nameof(people));
            }

            var log = logger.With("people", people);

            var stopwatch = new Stopwatch();

            //Start
            stopwatch.Start();

            var personSearchJob = new PersonSearchJob();
            Repository.Create(personSearchJob);
            await Repository.SaveAsync();

            log.With("PersonSearchJob.Id", personSearchJob.Id);

            log.InformationEvent("Run", "Processing started", stopwatch.ElapsedMilliseconds);

            try
            {
                foreach (var person in people)
                {
                    var result = await FindPersonController.GetFindPerson(person);

                    log.With("PersonSearchJob.Id", personSearchJob.Id);

                    log.With("Person", person)
                        .InformationEvent("Run", "Created PersonSearchJob Record after {ms}ms", stopwatch.ElapsedMilliseconds);

                    var jObject = JObject.Parse(result.Content);

                    var exportToJsonFileTask = Task.Run(async () => await this.Export.ToJsonAsync(jObject, $"SearchJob-{personSearchJob.Id}-{person.Name}", cancellationToken));

                    var parseAndSaveSearchTask = Task.Run(async () =>
                    {
                        var personObj = (JObject)jObject["person"];

                        log.With("personObj", personObj);

                        var search = new PersonSearchResult();
                        search.PersonSearchJobId = personSearchJob.Id;
                        search.HttpStatusCode = result.StatusCode;
                        search.NumberOfResults = (int)jObject["count_person"];
                        search.Warnings = (string)jObject["warnings"];
                        search.Error = (string)jObject["error"];
                        search.Data = personObj.ToString();

                        log.With("PersonSearchResult", search);

                        #region Log Data Problems

                        if (!string.IsNullOrWhiteSpace(search.Warnings))
                        {
                            log.WarningEvent("Run", "FindPerson api result returned with warning messages");
                        }
                        if (!string.IsNullOrWhiteSpace(search.Error))
                        {
                            log.ErrorEvent("Run", "FindPerson api result returned with error message");
                        }
                        if (string.IsNullOrWhiteSpace(search.Data))
                        {
                            log.ErrorEvent("Run", "FindPerson api result returned with no person data");
                        }

                        #endregion

                        Repository.Create(search);

                        await Repository.SaveAsync();
                    });

                    Task.WaitAll(exportToJsonFileTask, parseAndSaveSearchTask);
                    progress.Report(person);


                    log.DebugEvent("Run", "Finished processing person search result after {ms}ms", stopwatch.ElapsedMilliseconds);
                }

                //Complete
                personSearchJob.IsSuccessful = true;

                log.DebugEvent("Run", "Sucessfully processed {peopleCount} people after {ms}ms", people.Count(), stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                log.FatalEvent(ex, "Run", "Processing failed at {peopleCount} people after {ms}ms", people.Count(), stopwatch.ElapsedMilliseconds);

                personSearchJob.IsSuccessful = false;
            }

            //Finish
            personSearchJob.IsProcessed = true;
            Repository.Update(personSearchJob);
            Repository.Save();

            stopwatch.Stop();

            log.InformationEvent( "Run", "Processing person search job finished after {ms}ms", stopwatch.ElapsedMilliseconds);

            return personSearchJob.IsSuccessful;
        }
    }
}
