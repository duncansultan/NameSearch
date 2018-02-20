using McMaster.Extensions.CommandLineUtils;
using NameSearch.App.Commands;

namespace NameSearch.App.CommandConfigurations
{
    /// <summary>
    /// Import Searches Command Configuration
    /// </summary>
    public static class ProcessResultsCommandConfiguration
    {
        /// <summary>
        /// Configures the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="options">The options.</param>
        public static void Configure(CommandLineApplication command, CommandLineOptions options)
        {
            command.Description = "Process search results";
            command.HelpOption("--help|-h|-?");

            //todo
            bool isReprocess = false;

            command.OnExecute(() =>
            {
                options.Command = new ProcessResultsCommand(isReprocess, options);

                return 0;
            });
        }
    }
}