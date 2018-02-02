using AutoMapper;
using NameSearch.App.Helpers;
using NameSearch.Repository.Interfaces;
using NameSearch.Utility.Interfaces;
using Newtonsoft.Json;
using Serilog;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace NameSearch.App.Services
{
    /// <summary>
    /// Run File Imports and Exports
    /// </summary>
    public class ImportExport
    {
        #region Dependencies

        /// <summary>
        /// The export
        /// </summary>
        private readonly IExport Export;

        /// <summary>
        /// The import
        /// </summary>
        private readonly IImport Import;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger = Log.Logger.ForContext<ImportExport>();

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
        /// Initializes a new instance of the <see cref="ImportExport" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="serializerSettings">The serializer settings.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="import">The import.</param>
        /// <param name="export">The export.</param>
        public ImportExport(IEntityFrameworkRepository repository,
            JsonSerializerSettings serializerSettings,
            IMapper mapper,
            IImport import,
            IExport export)
        {
            this.Repository = repository;
            this.SerializerSettings = serializerSettings;
            this.Mapper = mapper;
            this.Import = import;
            this.Export = export;

            this.PersonSearchResultHelper = new PersonSearchResultHelper(repository, serializerSettings, mapper);
            this.PersonSearchJobHelper = new PersonSearchJobHelper(repository, mapper);
            this.PersonHelper = new PersonHelper(repository, mapper);
            this.NameHelper = new NameHelper(repository, mapper);
        }

        /// <summary>
        /// Exports the people.
        /// </summary>
        /// <param name="fullPath">Name of the file.</param>
        public void ExportPeople(string fullPath)
        {
            var people = PersonHelper.GetPeople();
            Export.ToCsv(people, fullPath, false);
        }

        /// <summary>
        /// Imports the names.
        /// </summary>
        /// <param name="fullPath">Name of the file.</param>
        /// <returns></returns>
        public long ImportNames(string fullPath)
        {
            var names = Import.FromCsv<string>(fullPath);
            var importId = NameHelper.Import(names, fullPath);
            return importId;
        }

        /// <summary>
        /// Imports the person searches from json.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
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
    }
}