using AutoMapper;
using Microsoft.Extensions.Configuration;
using NameSearch.Api.Controllers;
using NameSearch.Api.Controllers.Interfaces;
using NameSearch.App.Factories;
using NameSearch.App.Helpers;
using NameSearch.App.Helpers.Interfaces;
using NameSearch.Extensions;
using NameSearch.Models.Domain;
using NameSearch.Repository.Interfaces;
using NameSearch.Utility;
using NameSearch.Utility.Interfaces;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace NameSearch.App.Services
{
    /// <summary>
    /// Run Searches to Find People
    /// </summary>
    public class PeopleSearchService : IPeopleSearchService
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
        /// The import
        /// </summary>
        private readonly IImport Import;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger = Log.Logger.ForContext<PeopleSearchService>();

        /// <summary>
        /// The people search person helper
        /// </summary>
        private readonly IPersonHelper PersonHelper;

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration Configuration;

        /// <summary>
        /// The people search request helper
        /// </summary>
        private readonly IPersonSearchRequestHelper PersonSearchRequestHelper;

        /// <summary>
        /// The person search result helper
        /// </summary>
        private readonly IPersonSearchResultHelper PersonSearchResultHelper;

        /// <summary>
        /// The search wait ms
        /// </summary>
        private readonly int SearchWaitMs;

        #endregion Dependencies

        /// <summary>
        /// Initializes a new instance of the <see cref="PeopleFinder" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="findPersonController">The find person controller.</param>
        public PeopleSearchService(IConfiguration configuration, 
            IFindPersonController findPersonController,
            IPersonHelper personHelper,
            IPersonSearchRequestHelper personSearchRequestHelper,
            IPersonSearchResultHelper personSearchResultHelper,
            IExport export,
            IImport import)
        {
            this.Configuration = configuration;
            this.FindPersonController = findPersonController;
            this.Export = export;
            this.Import = import;

            this.PersonHelper = personHelper;
            this.PersonSearchRequestHelper = personSearchRequestHelper;
            this.PersonSearchResultHelper = personSearchResultHelper;

            this.SearchWaitMs = 60000;
            var waitMs = Configuration.GetValue<string>("SearchSettings:WaitMs");
            int.TryParse(waitMs, out this.SearchWaitMs);
        }

        /// <summary>
        /// Exports the people.
        /// </summary>
        /// <param name="fullPath">Name of the file.</param>
        public void ExportToCsv(string fullPath)
        {
            var people = PersonHelper.GetPeople();
            Export.ToCsv(people, fullPath, false);
        }

        /// <summary>
        /// Imports the searches.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        public void ImportSearches(string folderPath)
        {
            foreach (string fullPath in Directory.EnumerateFiles(folderPath, "*.json", SearchOption.AllDirectories))
            {
                var fileName = Path.GetFileName(fullPath);
                var jObject = Import.FromJson(fullPath);
                var personSearch = PersonSearchResultHelper.Import(fileName, jObject);
            }
        }

        /// <summary>
        /// Processes the results.
        /// </summary>
        /// <param name="reprocess">if set to <c>true</c> [reprocess].</param>
        public void ProcessResults(bool reprocess = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Searches the specified search criteria.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">searchCriteria</exception>
        public async Task<bool> SearchAsync(IEnumerable<Search> searches, string resultOutputPath, CancellationToken cancellationToken)
        {
            if (searches == null)
            {
                throw new ArgumentNullException(nameof(searches));
            }

            int runs = 0;
            foreach (var search in searches)
            {
                if (runs > search.MaxRuns)
                {
                    logger.InformationEvent("SearchAsync", "Search stopped after exceeding maximum of {maxRuns}", search.MaxRuns);
                    break;
                }

                var personSearch = await PersonSearchRequestHelper.SearchAsync(search, resultOutputPath, SearchWaitMs, cancellationToken);

                logger.InformationEvent("SearchAsync", "Search number {run} returned {numberOfResults} results", runs, personSearch.NumberOfResults);
                runs++;
            }

            return true;
        }
    }
}