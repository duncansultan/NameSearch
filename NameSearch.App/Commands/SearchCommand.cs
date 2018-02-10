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
        /// The path
        /// </summary>
        private readonly string _path;

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
        /// <param name="path">The path.</param>
        /// <param name="options">The options.</param>
        public SearchCommand(string maxRunsText, string city, string state, string zip, string path, CommandLineOptions options)
        {
            int.TryParse(maxRunsText, out int maxRuns);
            _maxRuns = maxRuns;
            _city = city;
            _state = state;
            _zip = zip;
            _path = path;
            _options = options;

            this.Repository = Program.Repository;
            this.Configuration = Program.Configuration;
            this.Export = new Export();
            this.Mapper = MapperFactory.Get();
            this.SerializerSettings = JsonSerializerSettingsFactory.Get();
            //ToDo: Get from AppConfig
            this.SearchWaitMs = 60000;

            this.PeopleSearch = new PeopleSearch(Repository, FindPersonController, SerializerSettings, Mapper, Export, SearchWaitMs);
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        public int Run()
        {
            var searchCriteria = SearchCriteriaFactory.Get(_maxRuns, _city, _state, _zip);

            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                Task.Run(() => PeopleSearchTask(searchCriteria, cancellationTokenSource.Token));
            }

            return 0;
        }

        /// <summary>
        /// Peoples the search task.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        private async Task PeopleSearchTask(SearchCriteria searchCriteria, CancellationToken cancellationToken)
            => await this.PeopleSearch.SearchAsync(searchCriteria, cancellationToken);
    }
}