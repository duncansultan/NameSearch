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

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportSearchesCommand" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="options">The options.</param>
        public ImportSearchesCommand(string path, CommandLineOptions options)
        {
            _path = path;
            _options = options;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        public int Run(IPeopleSearchService peopleSearchService)
        {
            peopleSearchService.ImportSearches(_path);

            return 0;
        }
    }
}