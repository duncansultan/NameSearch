using McMaster.Extensions.CommandLineUtils;
using NameSearch.App.Commands;

namespace NameSearch.App.CommandConfigurations
{
    /// <summary>
    /// Import Names Command Configuration
    /// </summary>
    public static class ImportNamesCommandConfiguration
    {
        /// <summary>
        /// Configures the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="options">The options.</param>
        public static void Configure(CommandLineApplication command, CommandLineOptions options)
        {
            command.Description = "Import names from csv";
            command.HelpOption("--help|-h|-?");

            var fullPathArgument = command.Argument("fullpath",
                                   "Full file path for import");

            var pathArgument = command.Argument("path",
                "File path for import");

            var fileNameArgument = command.Argument("filename",
                       "File name for import");

            command.OnExecute(() =>
            {
                options.Command = new ImportNamesCommand(fullPathArgument.Value, pathArgument.Value, fileNameArgument.Value, options);

                return 0;
            });
        }
    }
}