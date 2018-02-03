﻿using AutoMapper;
using NameSearch.Api.Controllers.Interfaces;
using NameSearch.App.Helpers;
using NameSearch.Extensions;
using NameSearch.Models.Domain;
using NameSearch.Models.Entities;
using NameSearch.Repository.Interfaces;
using NameSearch.Utility.Interfaces;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NameSearch.App.Services
{
    /// <summary>
    /// Run Searches to Find People
    /// </summary>
    public class PeopleSearch
    {
        #region Dependencies

        /// <summary>
        /// The find person controller
        /// </summary>
        private readonly IFindPersonController FindPersonController;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger = Log.Logger.ForContext<PeopleSearch>();

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper Mapper;

        /// <summary>
        /// The people search job helper
        /// </summary>
        private readonly PersonSearchJobHelper PersonSearchJobHelper;

        /// <summary>
        /// The people search request helper
        /// </summary>
        private readonly PersonSearchRequestHelper PersonSearchRequestHelper;

        /// <summary>
        /// The people search result helper
        /// </summary>
        private readonly PersonSearchResultHelper PersonSearchResultHelper;

        /// <summary>
        /// The repository
        /// </summary>
        private readonly IEntityFrameworkRepository Repository;

        /// <summary>
        /// The serializer settings
        /// </summary>
        private readonly JsonSerializerSettings SerializerSettings;

        /// <summary>
        /// The search wait ms
        /// </summary>
        private readonly int SearchWaitMs;

        #endregion Dependencies

        /// <summary>
        /// Initializes a new instance of the <see cref="PeopleFinder" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="findPersonController">The find person controller.</param>
        /// <param name="serializerSettings">The serializer settings.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="export">The export.</param>
        /// <param name="searchWaitMs">The search wait ms.</param>
        public PeopleSearch(IEntityFrameworkRepository repository,
            IFindPersonController findPersonController,
            JsonSerializerSettings serializerSettings,
            IMapper mapper,
            IExport export,
            int searchWaitMs)
        {
            this.Repository = repository;
            this.FindPersonController = findPersonController;
            this.SerializerSettings = serializerSettings;
            this.Mapper = mapper;

            this.SearchWaitMs = searchWaitMs;

            this.PersonSearchRequestHelper = new PersonSearchRequestHelper(repository, findPersonController, serializerSettings, mapper, export);
            this.PersonSearchResultHelper = new PersonSearchResultHelper(repository, serializerSettings, mapper);
            this.PersonSearchJobHelper = new PersonSearchJobHelper(repository, mapper);
        }

        /// <summary>
        /// Searches the specified search criteria.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">searchCriteria</exception>
        public async Task<bool> SearchAsync(SearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            if (searchCriteria == null)
            {
                throw new ArgumentNullException(nameof(searchCriteria));
            }
            var nameImport = Repository.GetFirst<NameImport>(orderBy: o => o.OrderByDescending(y => y.Id));
            var names = nameImport.Names.Select(x => x.Value).ToList();
            var peopleSearchJobId = PersonSearchJobHelper.CreateWithRequests(searchCriteria, names);
            var personSearchRequests = PersonSearchRequestHelper.Get(peopleSearchJobId);

            int runs = 0;
            foreach (var personSearchRequest in personSearchRequests)
            {
                if (runs > searchCriteria.MaxRuns)
                {
                    logger.InformationEvent("SearchAsync", "Search stopped after exceeding maximum of {maxRuns}", searchCriteria.MaxRuns);
                    break;
                }

                var personSearchResult = await PersonSearchRequestHelper.SearchAsync(personSearchRequest, SearchWaitMs, cancellationToken);

                logger.InformationEvent("SearchAsync", "Search number {run} returned {numberOfResults} results", runs, personSearchResult.NumberOfResults);

                var people = await PersonSearchResultHelper.ProcessAsync(personSearchResult, cancellationToken);
                runs++;
            }

            PersonSearchJobHelper.Complete(peopleSearchJobId);

            return true;
        }
    }
}