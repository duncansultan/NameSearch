using AutoMapper;
using Microsoft.Extensions.Configuration;
using NameSearch.Api.Controllers;
using NameSearch.Api.Controllers.Interfaces;
using NameSearch.App.Commands.Interfaces;
using NameSearch.App.Factories;
using NameSearch.App.Services;
using NameSearch.Models.Domain;
using NameSearch.Repository.Interfaces;
using NameSearch.Utility;
using NameSearch.Utility.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NameSearch.App.Commands
{
    /// <summary>
    /// Search Command
    /// </summary>
    /// <seealso cref="NameSearch.App.Commands.Interfaces.ICommand" />
    public class SearchCommand : ICommand
    {
        /// <summary>
        /// The maximum runs
        /// </summary>
        private readonly int _maxRuns;

        /// <summary>
        /// The city
        /// </summary>
        private readonly string _city;

        /// <summary>
        /// The state
        /// </summary>
        private readonly string _state;

        /// <summary>
        /// The zip
        /// </summary>
        private readonly string _zip;

        /// <summary>
        /// The names file path
        /// </summary>
        private readonly string _namesFilePath;

        /// <summary>
        /// The result output path
        /// </summary>
        private readonly string _resultOutputPath;

        /// <summary>
        /// The options
        /// </summary>
        private readonly CommandLineOptions _options;

        #region Dependencies

        /// <summary>
        /// The import
        /// </summary>
        private readonly IImport Import;

        /// <summary>
        /// The export
        /// </summary>
        private readonly IExport Export;

        /// <summary>
        /// The mapper
        /// </summary>
        /// <value>
        /// The mapper.
        /// </value>
        public IMapper Mapper { get; set; }

        /// <summary>
        /// The repository
        /// </summary>
        /// <value>
        /// The repository.
        /// </value>
        public IEntityFrameworkRepository Repository { get; set; }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// The serializer settings
        /// </summary>
        /// <value>
        /// The serializer settings.
        /// </value>
        public JsonSerializerSettings SerializerSettings { get; set; }

        /// <summary>
        /// Gets or sets the find person controller.
        /// </summary>
        /// <value>
        /// The find person controller.
        /// </value>
        public IFindPersonController FindPersonController { get; set; }

        /// <summary>
        /// The people search
        /// </summary>
        private readonly PeopleSearch PeopleSearch;

        /// <summary>
        /// The search wait ms
        /// </summary>
        private readonly int SearchWaitMs;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchCommand" /> class.
        /// </summary>
        /// <param name="maxRunsText">The maximum runs text.</param>
        /// <param name="city">The city.</param>
        /// <param name="state">The state.</param>
        /// <param name="zip">The zip.</param>
        /// <param name="namesFilePath">The names text file path.</param>
        /// <param name="resultOutputPath">The temporary path.</param>
        /// <param name="options">The options.</param>
        public SearchCommand(string maxRunsText, string city, string state, string zip, string namesFilePath, string resultOutputPath, CommandLineOptions options)
        {
            int.TryParse(maxRunsText, out int maxRuns);
            _maxRuns = maxRuns;
            _city = city;
            _state = state;
            _zip = zip;
            _namesFilePath = namesFilePath;
            _resultOutputPath = resultOutputPath;
            _options = options;

            this.Repository = Program.Repository;
            this.Configuration = Program.Configuration;
            this.FindPersonController = new FindPersonController(this.Configuration);
            this.Import = new Import();
            this.Export = new Export();
            this.Mapper = MapperFactory.Get();
            this.SerializerSettings = JsonSerializerSettingsFactory.Get();

            
            //Default Value
            this.SearchWaitMs = 60000;
            var waitMs = Configuration.GetValue<string>("SearchSettings:WaitMs");
            int.TryParse(waitMs, out this.SearchWaitMs);
            

            this.PeopleSearch = new PeopleSearch(Repository, FindPersonController, SerializerSettings, Mapper, Export, _resultOutputPath, SearchWaitMs);
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        public int Run()
        {
            var cancelAfterMs = 600000;

            var searchCriteria = SearchCriteriaFactory.Get(_maxRuns, _city, _state, _zip);

            var names = Import.FromTxt(_namesFilePath);

            using (var cancellationTokenSource = new CancellationTokenSource(cancelAfterMs))
            {
                var token = cancellationTokenSource.Token;
                Task.Run(() => PeopleSearchTask(searchCriteria, names, token));
            }

            return 0;
        }

        /// <summary>
        /// Peoples the search task.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        private async Task PeopleSearchTask(SearchCriteria searchCriteria, IEnumerable<string> names, CancellationToken cancellationToken)
            => await this.PeopleSearch.SearchAsync(searchCriteria, names, cancellationToken);
    }
}