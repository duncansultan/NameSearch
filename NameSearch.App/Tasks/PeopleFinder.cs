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
        private ILogger logger = Log.Logger.ForContext<PeopleFinder>();
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
        public async Task<bool> Run(IEnumerable<Models.Domain.Api.Request.Person> people, IProgress<Models.Domain.Api.Request.Person> progress, CancellationToken cancellationToken)
        {
            if (people == null || !people.Any())
            {
                throw new ArgumentNullException(nameof(people));
            }

            var stopwatch = new Stopwatch();
            
            //Start
            stopwatch.Start();

            var personSearchJob = new PersonSearchJob();
            Repository.Create(personSearchJob);
            await Repository.SaveAsync();

            logger.ForContext("people", people)
                .ForContext("PersonSearchJob.Id", personSearchJob.Id)
                .Information("<{EventID:l}> - {message} - {ms} ms", "Run", "Processing started", stopwatch.ElapsedMilliseconds);

            try
            {
                foreach (var person in people)
                {
                    var result = await FindPersonController.GetFindPerson(person);

                    logger.ForContext("Person", person, true)
                        .ForContext("RequestUri", result.RequestUri)
                        .ForContext("StatusCode", result.StatusCode)
                        .Information("<{EventID:l}> - {message} - {ms} ms", "Run", "Created PersonSearchJob Record", stopwatch.ElapsedMilliseconds);

                    var jObject = JObject.Parse(result.Content);

                    var exportToJsonFileTask = Task.Run(async () => await this.Export.ToJsonAsync(jObject, $"SearchJob-{personSearchJob.Id}-{person.Name}", cancellationToken));

                    var parseAndSaveSearchTask = Task.Run(async () =>
                    {
                        var personObj = (JObject)jObject["person"];

                        var search = new PersonSearchResult();
                        search.PersonSearchJobId = personSearchJob.Id;
                        search.HttpStatusCode = result.StatusCode;
                        search.NumberOfResults = (int)jObject["count_person"];
                        search.Warnings = (string)jObject["warnings"];
                        search.Error = (string)jObject["error"];
                        search.Data = personObj.ToString();

                        #region Log Data Problems

                        if (!string.IsNullOrWhiteSpace(search.Warnings))
                        {
                            logger.ForContext("personObj", personObj)
                                .ForContext("PersonSearchResult", search)
                                .ForContext("Warnings", search.Warnings)
                                .Warning("<{EventID:l}> - {message} - {ms} ms", "Run", "FindPerson api result returned with warning messages", stopwatch.ElapsedMilliseconds);
                        }
                        if (!string.IsNullOrWhiteSpace(search.Error))
                        {
                            logger.ForContext("personObj", personObj)
                                .ForContext("Error", search)
                                .ForContext("Warnings", search.Error)
                                .Error("<{EventID:l}> - {message} - {ms} ms", "Run", "FindPerson api result returned with error messages", stopwatch.ElapsedMilliseconds);
                        }
                        if (string.IsNullOrWhiteSpace(search.Data))
                        {
                            logger.ForContext("personObj", personObj)
                                .ForContext("Error", search)
                                .ForContext("Warnings", search.Error)
                                .Error("<{EventID:l}> - {message} - {ms} ms", "Run", "FindPerson api result returned with no person data", stopwatch.ElapsedMilliseconds);
                        }

                        #endregion

                        Repository.Create(search);

                        await Repository.SaveAsync();                        
                    });

                    Task.WaitAll(exportToJsonFileTask, parseAndSaveSearchTask);
                    progress.Report(person);

                    logger.ForContext("Person", person, true)
                        .Information("<{EventID:l}> - {message} - {ms} ms", "Run", "Finished processing person", stopwatch.ElapsedMilliseconds);
                }

                //Complete
                personSearchJob.IsSuccessful = true;

                logger.ForContext("people", people)
                    .ForContext("PersonSearchJob.Id", personSearchJob.Id)
                    .Information("<{EventID:l}> - {message} - {ms} ms", "Run", "Sucessfully processed all people", stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                logger.ForContext("people", people)
                    .ForContext("PersonSearchJob.Id", personSearchJob.Id)
                    .Fatal(ex, "<{EventID:l}> - {message} - {ms} ms", "Run", "Processing failed", stopwatch.ElapsedMilliseconds);

                personSearchJob.IsSuccessful = false;
            }

            //Finish
            personSearchJob.IsProcessed = true;
            Repository.Update(personSearchJob);
            Repository.Save();

            stopwatch.Stop();

            logger.ForContext("people", people)
                .ForContext("PersonSearchJob.Id", personSearchJob.Id)
                .Information("<{EventID:l}> - {message} - {ms} ms", "Run", "Processing finished", stopwatch.ElapsedMilliseconds);

            return personSearchJob.IsSuccessful;
        }
    }
}
