using McMaster.Extensions.CommandLineUtils;
using NameSearch.App.Commands.Interfaces;
using NameSearch.App.Services;

namespace NameSearch.App.Commands
{
    /// <summary>
    /// Root Command
    /// </summary>
    /// <seealso cref="NameSearch.App.Commands.Interfaces.ICommand" />
    public class RootCommand : ICommand
    {
        /// <summary>
        /// The application
        /// </summary>
        private readonly CommandLineApplication _app;

        /// <summary>
        /// Initializes a new instance of the <see cref="RootCommand"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        public RootCommand(CommandLineApplication app)
        {
            _app = app;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        public int Run(IPeopleSearchService peopleSearchService)
        {
            _app.ShowHelp();

            return 1;
        }
    }
}