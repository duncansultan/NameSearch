using McMaster.Extensions.CommandLineUtils;
using NameSearch.App.Commands;

namespace NameSearch.App.CommandConfigurations
{
    /// <summary>
    /// Import Searches Command Configuration
    /// </summary>
    public static class ImportSearchesCommandConfiguration
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
                options.Command = new ImportSearchesCommand(folderPath.Value, options);

                return 0;
            });
        }
    }
}