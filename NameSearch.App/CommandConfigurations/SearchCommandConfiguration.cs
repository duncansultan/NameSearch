using McMaster.Extensions.CommandLineUtils;
using NameSearch.App.Commands;

namespace NameSearch.App.CommandConfigurations
{
    /// <summary>
    /// Search Command Configuration
    /// </summary>
    public static class SearchCommandConfiguration
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

            var namesFilePath = command.Argument("namesFilePath",
                "File path for Search Names text file");

            var resultOutputPath = command.Argument("resultOutputPath",
                "Folder path for Search Results output");

            var maxRunsArgument = command.Argument("maxRuns",
                "Maximum number of times to search");

            command.OnExecute(() =>
            {
                options.Command = new SearchCommand(maxRunsArgument.Value, cityArgument.Value, stateArgument.Value, zipArgument.Value, namesFilePath.Value, resultOutputPath.Value, options);

                return 0;
            });
        }
    }
}