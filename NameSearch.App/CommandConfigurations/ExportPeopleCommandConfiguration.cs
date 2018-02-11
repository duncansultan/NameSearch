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
                                   "Full file path for import");

            command.OnExecute(() =>
            {
                options.Command = new ExportPeopleCommand(options);

                return 0;
            });
        }
    }
}