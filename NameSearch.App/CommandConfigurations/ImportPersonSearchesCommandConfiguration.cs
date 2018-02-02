using McMaster.Extensions.CommandLineUtils;
using NameSearch.App.Commands;

namespace NameSearch.App.CommandConfigurations
{
    /// <summary>
    /// Import Person Searches Command Configuration
    /// </summary>
    public static class ImportPersonSearchesCommandConfiguration
    {
        /// <summary>
        /// Configures the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="options">The options.</param>
        public static void Configure(CommandLineApplication command, CommandLineOptions options)
        {
            command.Description = "Import search result from json";
            command.HelpOption("--help|-h|-?");

            var folderPath = command.Argument("path",
                "Folder path for import");

            command.OnExecute(() =>
            {
                options.Command = new ImportPersonSearchesCommand(folderPath.Value, options);

                return 0;
            });
        }
    }
}