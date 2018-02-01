using McMaster.Extensions.CommandLineUtils;
using NameSearch.App.Commands;

namespace NameSearch.App.CommandConfigurations
{
    /// <summary>
    /// Export People Command Configuration
    /// </summary>
    public static class ExportPeopleCommandConfiguration
    {
        /// <summary>
        /// Configures the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="options">The options.</param>
        public static void Configure(CommandLineApplication command, CommandLineOptions options)
        {
            command.Description = "Export People to a csv file";
            command.HelpOption("--help|-h|-?");

            var fullPathArgument = command.Argument("fullpath",
                                   "Full file path for export");

            var pathArgument = command.Argument("path",
                "File path for export");

            var fileNameArgument = command.Argument("filename",
                       "File name for export");

            command.OnExecute(() =>
            {
                options.Command = new ExportPeopleCommand(fullPathArgument.Value, pathArgument.Value, fileNameArgument.Value, options);

                return 0;
            });
        }
    }
}