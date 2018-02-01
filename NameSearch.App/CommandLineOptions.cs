using McMaster.Extensions.CommandLineUtils;
using NameSearch.App.CommandConfiguration;
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

        public bool IsEnthousiastic { get; set; }

        public static CommandLineOptions Parse(string[] args)
        {
            var options = new CommandLineOptions();

            var app = new CommandLineApplication
            {
                Name = "console-starter",
                FullName = ".NET Core Neat Console Starter"
            };

            app.HelpOption("-?|-h|--help");

            var enthousiasticSwitch = app.Option("-e|--enthousiastically",
                                          "Whether the app should be enthousiastic.",
                                          CommandOptionType.NoValue);

            RootCommandConfiguration.Configure(app, options);

            var result = app.Execute(args);

            if (result != 0)
            {
                return null;
            }

            options.IsEnthousiastic = enthousiasticSwitch.HasValue();

            return options;
        }
    }
}