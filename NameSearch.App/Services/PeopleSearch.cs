using NameSearch.Repository;
using System.Threading.Tasks;
using NameSearch.Api.Controllers.Interfaces;
using Serilog;
using NameSearch.Utility.Interfaces;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using NameSearch.Extensions;
using NameSearch.Models.Utility;
using NameSearch.App.Services;
using NameSearch.Models.Entities;
using NameSearch.Models.Domain;
using Newtonsoft.Json;
using AutoMapper;

namespace NameSearch.App.Tasks
{
    /// <summary>
    /// Run Searches to Find People
    /// </summary>
    public class PeopleSearch
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger = Log.Logger.ForContext<PeopleSearch>();
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IEntityFrameworkRepository Repository;
        private readonly IImport Import;
        private readonly IExport Export;
        private readonly IFindPersonController FindPersonController;
        private readonly JsonSerializerSettings SerializerSettings;
        private readonly IMapper Mapper;
        private readonly PersonSearchRequestHelper PersonSearchRequestHelper;
        private readonly PersonSearchResultHelper PersonSearchResultHelper;
        private readonly PeopleSearchJobHelper PeopleSearchJobHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeopleFinder"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="findPersonController">The find person controller.</param>
        /// <param name="export">The export.</param>
        public PeopleSearch(IEntityFrameworkRepository repository,
            IFindPersonController findPersonController,
            JsonSerializerSettings serializerSettings,
            IMapper mapper,
            IImport import,
            IExport export)
        {
            this.Repository = repository;
            this.FindPersonController = findPersonController;
            this.SerializerSettings = serializerSettings;
            this.Mapper = mapper;
            this.Import = import;
            this.Export = export;

            this.PersonSearchRequestHelper = new PersonSearchRequestHelper(repository, findPersonController, serializerSettings, mapper, export);
            this.PersonSearchResultHelper = new PersonSearchResultHelper(repository, serializerSettings, mapper);
            this.PeopleSearchJobHelper = new PeopleSearchJobHelper(repository, mapper);
        }

        public async Task<bool> Search(SearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            if (searchCriteria == null)
            {
                throw new ArgumentNullException(nameof(searchCriteria));

            }
            var nameImport = Repository.GetFirst<NameImport>(orderBy: o => o.OrderByDescending(y => y.Id));
            var peopleSearchJobId = PeopleSearchJobHelper.Create(searchCriteria, nameImport.Id);
            var personSearchJob = PeopleSearchJobHelper.Get(peopleSearchJobId);

            foreach (var personSearchRequest in personSearchJob.PersonSearchRequests)
            {
                //ToDo: Add Logging and Progress Updates Here
                var personSearchResult = await PersonSearchRequestHelper.SearchAsync(personSearchRequest, cancellationToken);
                var person = await PersonSearchResultHelper.ProcessAsync(personSearchResult, cancellationToken);
            }

            PeopleSearchJobHelper.Complete(peopleSearchJobId);

            return true;
        }

        public void ExportPeople(string fileName)
        {
            //Export.ToCsv<T>(records, fileName, false);
        }

        public void ImportNames(string fileName)
        {
            //Import.FromCsv<T>(fileName);
        }
    }
}
