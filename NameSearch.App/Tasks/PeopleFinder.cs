using NameSearch.Repository;
using System.Threading.Tasks;
using NameSearch.Api.Controllers.Interfaces;
using Newtonsoft.Json.Linq;
using Serilog;
using NameSearch.Utility.Interfaces;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using NameSearch.Extensions;
using NameSearch.Models.Utility;

namespace NameSearch.App.Tasks
{
    /// <summary>
    /// Run Searches to Find People
    /// </summary>
    public class PeopleFinder : IPeopleFinder
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
        public async Task<bool> Run<TProgressReport>(IEnumerable<Models.Domain.Api.Request.IPerson> people, IProgress<TProgressReport> progress, CancellationToken cancellationToken) where TProgressReport : IProgressReport, new()
        {
            if (people == null || !people.Any())
            {
                throw new ArgumentNullException(nameof(people));
            }

            var log = logger.With("people", people);

            var stopwatch = new Stopwatch();

            var totalPeople = people.Count();

            //Start
            var progressReport = new TProgressReport
            {
                IsRunning = true,
                ProgressCount = 1,
                TotalCount = totalPeople
            };

            progress.Report(progressReport);

            stopwatch.Start();

            var personSearchJob = new Models.Entities.PersonSearchJob();
            Repository.Create(personSearchJob);
            await Repository.SaveAsync();

            log.With("PersonSearchJob.Id", personSearchJob.Id);

            log.InformationEvent("Run", "Processing started", stopwatch.ElapsedMilliseconds);
            progressReport.Message = "Processing started";
            progressReport.ElapsedTimeSpan = stopwatch.Elapsed;
            progress.Report(progressReport);

            try
            {
                foreach (var person in people)
                {
                    var result = await FindPersonController.GetFindPerson(person);

                    log.With("PersonSearchJob.Id", personSearchJob.Id);

                    log.With("Person", person)
                        .InformationEvent("Run", "Created PersonSearchJob Record after {ms}ms", stopwatch.ElapsedMilliseconds);

                    progressReport.Message = "Created PersonSearchJob Record";
                    progressReport.ElapsedTimeSpan = stopwatch.Elapsed;
                    progress.Report(progressReport);

                    var jObject = JObject.Parse(result.Content);

                    var exportToJsonFileTask = Task.Run(async () => await this.Export.ToJsonAsync(jObject, $"SearchJob-{personSearchJob.Id}-{person.Name}", cancellationToken));

                    var parseAndSaveSearchTask = Task.Run(async () =>
                    {
                        log.With("Content", jObject);

                        var search = new Models.Entities.PersonSearchResult();
                        search.PersonSearchJobId = personSearchJob.Id;
                        search.HttpStatusCode = result.StatusCode;
                        search.NumberOfResults = (int)jObject["count_person"];
                        search.Warnings = (string)jObject["warnings"];
                        search.Error = (string)jObject["error"];
                        search.Data = jObject.ToString();

                        log.With("PersonSearchResult", search);

                        #region Log Data Problems

                        if (!string.IsNullOrWhiteSpace(search.Warnings))
                        {
                            log.WarningEvent("Run", "FindPerson api result returned with warning messages");
                            progressReport.Message = "FindPerson api result returned with warning messages";
                            progressReport.ElapsedTimeSpan = stopwatch.Elapsed;
                            progress.Report(progressReport);
                        }
                        if (!string.IsNullOrWhiteSpace(search.Error))
                        {
                            log.ErrorEvent("Run", "FindPerson api result returned with error message");
                            progressReport.Message = "FindPerson api result returned with error message";
                            progressReport.ElapsedTimeSpan = stopwatch.Elapsed;
                            progress.Report(progressReport);
                        }
                        if (string.IsNullOrWhiteSpace(search.Data))
                        {
                            log.ErrorEvent("Run", "FindPerson api result returned with no person data");
                            progressReport.Message = "FindPerson api result returned with no person data";
                            progressReport.ElapsedTimeSpan = stopwatch.Elapsed;
                            progress.Report(progressReport);
                        }

                        #endregion

                        Repository.Create(search);

                        await Repository.SaveAsync();
                    });

                    Task.WaitAll(exportToJsonFileTask, parseAndSaveSearchTask);

                    log.DebugEvent("Run", "Finished processing person search result after {ms}ms", stopwatch.ElapsedMilliseconds);

                    progressReport.Message = "Finished processing person search result";
                    progressReport.UpdateRemaining();
                    progressReport.ElapsedTimeSpan = stopwatch.Elapsed;
                    progress.Report(progressReport);
                }

                //Complete
                personSearchJob.IsSuccessful = true;

                log.DebugEvent("Run", "Sucessfully processed {peopleCount} people after {ms}ms", people.Count(), stopwatch.ElapsedMilliseconds);

                progressReport.Message = "Finished processing person search result";
                progressReport.UpdateRemaining();
                progressReport.ElapsedTimeSpan = stopwatch.Elapsed;
                progress.Report(progressReport);
            }
            catch (Exception ex)
            {
                personSearchJob.IsSuccessful = false;

                log.FatalEvent(ex, "Run", "Processing failed at {peopleCount} people after {ms}ms", people.Count(), stopwatch.ElapsedMilliseconds);

                progressReport.Message = "Processing failed with an Exception";
                progressReport.ElapsedTimeSpan = stopwatch.Elapsed;
                progress.Report(progressReport);
            }

            //Finish
            personSearchJob.IsProcessed = true;
            Repository.Update(personSearchJob);
            Repository.Save();

            stopwatch.Stop();

            log.InformationEvent("Run", "Processing person search job finished after {ms}ms", stopwatch.ElapsedMilliseconds);

            progressReport.Message = "Processing person search job finished after";
            progressReport.UpdateRemaining();
            progressReport.ElapsedTimeSpan = stopwatch.Elapsed;
            progress.Report(progressReport);

            return personSearchJob.IsSuccessful;
        }
    }
}
