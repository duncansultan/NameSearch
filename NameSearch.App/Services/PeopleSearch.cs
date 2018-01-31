using AutoMapper;
using NameSearch.Api.Controllers.Interfaces;
using NameSearch.App.Helpers;
using NameSearch.App.Services;
using NameSearch.Models.Domain;
using NameSearch.Models.Entities;
using NameSearch.Repository;
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
        private readonly ILogger logger = Log.Logger.ForContext<PeopleSearch>();

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper Mapper;

        /// <summary>
        /// The people search name helper
        /// </summary>
        private readonly NameHelper NameHelper;

        /// <summary>
        /// The people search person helper
        /// </summary>
        private readonly PersonHelper PersonHelper;

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
        #endregion Dependencies

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
            this.PersonSearchJobHelper = new PersonSearchJobHelper(repository, mapper);
            this.PersonHelper = new PersonHelper(repository, mapper);
            this.NameHelper = new NameHelper(repository, mapper);
        }

        /// <summary>
        /// Exports the people.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void ExportPeople(string fileName)
        {
            var people = PersonHelper.GetPeople();
            Export.ToCsv(people, fileName, false);
        }

        /// <summary>
        /// Imports the names.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public long ImportNames(string fileName)
        {
            var names = Import.FromCsv<string>(fileName);
            var importId = NameHelper.Import(names, fileName);
            return importId;
        }

        /// <summary>
        /// Imports the person searches from json.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<int> ImportPersonSearchesFromJsonAsync(string fileName, CancellationToken cancellationToken)
        {
            var jObject = await Import.FromJsonAsync(fileName, cancellationToken);

            var peopleSearchJobId = PersonSearchJobHelper.Create();
            var personSearchResult = await PersonSearchResultHelper.ImportAsync(jObject, peopleSearchJobId);
            var people = await PersonSearchResultHelper.ProcessAsync(personSearchResult, cancellationToken);
            PersonSearchJobHelper.Complete(peopleSearchJobId);

            return people.Count();
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

            foreach (var personSearchRequest in personSearchRequests)
            {
                //ToDo: Add Logging and Progress Updates Here
                var personSearchResult = await PersonSearchRequestHelper.SearchAsync(personSearchRequest, cancellationToken);
                var person = await PersonSearchResultHelper.ProcessAsync(personSearchResult, cancellationToken);
            }

            PersonSearchJobHelper.Complete(peopleSearchJobId);

            return true;
        }
    }
}