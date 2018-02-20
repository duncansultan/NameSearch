using NameSearch.App.Commands.Interfaces;
using NameSearch.App.Factories;
using NameSearch.App.Services;
using NameSearch.Models.Domain;
using NameSearch.Utility;
using NameSearch.Utility.Interfaces;
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
        /// The people search
        /// </summary>
        private readonly PeopleSearch PeopleSearch;

        /// <summary>
        /// The import
        /// </summary>
        private readonly IImport Import;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchCommand" /> class.
        /// </summary>
        /// <param name="maxRunsText">The maximum runs text.</param>
        /// <param name="city">The city.</param>
        /// <param name="state">The state.</param>
        /// <param name="zip">The zip.</param>
        /// <param name="namesFilePath">The names text file path.</param>
        /// <param name="options">The options.</param>
        public SearchCommand(string maxRunsText, string city, string state, string zip, string namesFilePath, CommandLineOptions options)
        {
            int.TryParse(maxRunsText, out int maxRuns);
            _maxRuns = maxRuns;
            _city = city;
            _state = state;
            _zip = zip;
            _namesFilePath = namesFilePath;
            _resultOutputPath = Program.SearchResultsDirectory;
            _options = options;

            this.Import = new Import();
            this.PeopleSearch = new PeopleSearch(Program.Repository, Program.Configuration);
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        public int Run()
        {
            var cancelAfterMs = 600000;

            var names = Import.FromTxt(_namesFilePath);
            var searchCriteria = SearchCriteriaFactory.Get(_city, _state, _zip);
            var searches = SearchesFactory.Get(searchCriteria, names, _maxRuns);

            using (var cancellationTokenSource = new CancellationTokenSource(cancelAfterMs))
            {
                var token = cancellationTokenSource.Token;
                Task.Run(() => PeopleSearchTask(searches, _resultOutputPath, token));
            }

            return 0;
        }

        /// <summary>
        /// Peoples the search task.
        /// </summary>
        /// <param name="searches">The searches.</param>
        /// <param name="resultOutputPath">The result output path.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        private async Task PeopleSearchTask(IEnumerable<Search> searches, string resultOutputPath, CancellationToken cancellationToken)
            => await this.PeopleSearch.SearchAsync(searches, resultOutputPath, cancellationToken);
    }
}