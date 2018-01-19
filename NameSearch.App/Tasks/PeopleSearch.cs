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

namespace NameSearch.App.Tasks
{
    /// <summary>
    /// Run Searches for People
    /// </summary>
    public class PeopleSearch
    {
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
        /// Initializes a new instance of the <see cref="PeopleSearch"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="findPersonController">The find person controller.</param>
        /// <param name="export">The export.</param>
        public PeopleSearch(IEntityFrameworkRepository repository,
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

            //Start
            var searchJob = new SearchJob();
            Repository.Create(searchJob);
            await Repository.SaveAsync();

            try
            {
                foreach (var person in people)
                {
                    var result = await FindPersonController.GetPerson(person);
                    var jObject = JObject.Parse(result.Value.ToString());
                    
                    var exportToJsonFileTask = Task.Run(async () =>
                    {
                        await this.Export.ToJsonAsync(jObject, $"SearchJob-{searchJob.Id}-{person.Name}", cancellationToken);
                    });

                    var parseAndSaveSearchTask = Task.Run(async () =>
                    {
                        
                        var personObj = (JObject)jObject["person"];

                        var search = new SearchTransaction();
                        search.SearchJobId = searchJob.Id;
                        search.HttpStatusCode = result.StatusCode;
                        search.NumberOfResults = (int)jObject["count_person"];
                        search.Warnings = (string)jObject["warnings"];
                        search.Error = (string)jObject["error"];
                        search.Data = personObj.ToString();
                    
                        Repository.Create(search);
                        await Repository.SaveAsync();

                        #region Log Data Problems

                        if (!string.IsNullOrWhiteSpace(search.Warnings))
                        {
                            Log.Warning("FindPerson api result returned with warning messages.", search.Warnings);
                        }

                        if (!string.IsNullOrWhiteSpace(search.Error))
                        {
                            Log.Error("FindPerson api result returned with error messages.", search.Error);
                        }

                        if (string.IsNullOrWhiteSpace(search.Data))
                        {
                            Log.Error("FindPerson api result returned with no person data.");
                        }

                        #endregion
                    });
                    Task.WaitAll(exportToJsonFileTask, parseAndSaveSearchTask);
                    progress.Report(person);
                }

                searchJob.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                Log.Warning(ex, $"SearchJobId {searchJob.Id} was not successful.");
                searchJob.IsSuccessful = false;
            }

            //Finish
            searchJob.IsFinished = true;
            Repository.Update(searchJob);
            Repository.Save();

            return searchJob.IsSuccessful;
        }
    }
}
