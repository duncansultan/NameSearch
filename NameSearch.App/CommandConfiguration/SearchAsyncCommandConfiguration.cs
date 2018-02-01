using McMaster.Extensions.CommandLineUtils;
using NameSearch.App.Commands;

namespace NameSearch.App.CommandConfiguration
{
    /// <summary>
    /// Search Command Configuration
    /// </summary>
    public static class SearchAsyncCommandConfiguration
    {
        /// <summary>
        /// Configures the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="options">The options.</param>
        public static void Configure(CommandLineApplication command, CommandLineOptions options)
        {
            command.Description = "Search for people";
            command.HelpOption("--help|-h|-?");

            var cityArgument = command.Argument("city",
                                   "City to search");

            var stateArgument = command.Argument("state",
                       "State to search");

            var zipArgument = command.Argument("zip",
                       "Zip to search");

            command.OnExecute(() =>
            {
                options.Command = new SearchAsyncCommand(cityArgument.Value, stateArgument.Value, zipArgument.Value, options);

                return 0;
            });
        }
    }
}