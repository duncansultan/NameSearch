using McMaster.Extensions.CommandLineUtils;
using NameSearch.App.CommandConfigurations;
using NameSearch.App.Commands.Interfaces;

namespace NameSearch.App
{
    /// <summary>
    /// Command Line Options
    /// </summary>
    public class CommandLineOptions
    {
        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        public ICommand Command { get; set; }

        /// <summary>
        /// Parses the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static CommandLineOptions Parse(string[] args)
        {
            var options = new CommandLineOptions();

            var app = new CommandLineApplication
            {
                Name = "people-search",
                FullName = "People Search Console"
            };

            app.HelpOption("-?|-h|--help");

            RootCommandConfiguration.Configure(app, options);

            var result = app.Execute(args);

            if (result != 0)
            {
                return null;
            }

            return options;
        }
    }
}