using NameSearch.App.Commands.Interfaces;
using NameSearch.App.Services;

namespace NameSearch.App.Commands
{
    /// <summary>
    /// Process Search Results Command
    /// </summary>
    /// <seealso cref="NameSearch.App.Commands.Interfaces.ICommand" />
    public class ProcessResultsCommand : ICommand
    {
        /// <summary>
        /// The is reprocess
        /// </summary>
        private readonly bool _isReprocess;

        /// <summary>
        /// The options
        /// </summary>
        private readonly CommandLineOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessResultsCommand"/> class.
        /// </summary>
        /// <param name="isReprocess">if set to <c>true</c> [is reprocess].</param>
        /// <param name="options">The options.</param>
        public ProcessResultsCommand(bool isReprocess, CommandLineOptions options)
        {
            _options = options;

            this._isReprocess = isReprocess;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        public int Run(IPeopleSearchService peopleSearchService)
        {
            peopleSearchService.ProcessResults(_isReprocess);

            return 0;
        }
    }
}