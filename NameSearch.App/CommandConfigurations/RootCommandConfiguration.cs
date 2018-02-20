﻿using McMaster.Extensions.CommandLineUtils;
using NameSearch.App.Commands;

namespace NameSearch.App.CommandConfigurations
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
            app.Command("exportpeople", c => ExportPeopleCommandConfiguration.Configure(c, options));
            app.Command("importsearches", c => ImportSearchesCommandConfiguration.Configure(c, options));
            app.Command("search", c => SearchCommandConfiguration.Configure(c, options));
            app.Command("processresults", c => ProcessResultsCommandConfiguration.Configure(c, options));

            app.OnExecute(() =>
            {
                options.Command = new RootCommand(app);

                return 0;
            });
        }
    }
}