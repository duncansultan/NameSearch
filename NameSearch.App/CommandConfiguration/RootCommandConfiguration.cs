using McMaster.Extensions.CommandLineUtils;
using NameSearch.App.Commands;

namespace NameSearch.App.CommandConfiguration
{
    /// <summary>
    /// Root Command Configuration
    /// </summary>
    public static class RootCommandConfiguration
    {
        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="options">The options.</param>
        public static void Configure(CommandLineApplication app, CommandLineOptions options)
        {
            app.Command("greet", c => GreetCommandConfiguration.Configure(c, options));
            app.Command("exportpeople", c => ExportPeopleCommandConfiguration.Configure(c, options));
            app.Command("importnames", c => ImportNamesCommandConfiguration.Configure(c, options));
            app.Command("importsearchresult", c => ImportPersonSearchesFromJsonAsyncCommandConfiguration.Configure(c, options));
            app.Command("search", c => SearchAsyncCommandConfiguration.Configure(c, options));

            app.OnExecute(() =>
            {
                options.Command = new RootCommand(app);

                return 0;
            });
        }
    }
}