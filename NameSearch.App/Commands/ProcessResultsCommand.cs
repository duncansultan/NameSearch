using NameSearch.App.Commands.Interfaces;
using NameSearch.App.Services;

namespace NameSearch.App.Commands
{
    /// <summary>
    /// Import Searches Command
    /// </summary>
    /// <seealso cref="NameSearch.App.Commands.Interfaces.ICommand" />
    public class ImportSearchesCommand : ICommand
    {
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
        /// The import export
        /// </summary>
        private readonly PeopleSearch PeopleSearch;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportSearchesCommand" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="options">The options.</param>
        public ImportSearchesCommand(string path, CommandLineOptions options)
        {
            _path = path;
            _options = options;

            this.PeopleSearch = new PeopleSearch(Program.Repository, Program.Configuration);
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        public int Run()
        {
            this.PeopleSearch.ImportSearches(_path);

            return 0;
        }
    }
}