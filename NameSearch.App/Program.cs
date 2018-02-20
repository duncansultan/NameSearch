using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NameSearch.Api.Controllers;
using NameSearch.Api.Controllers.Interfaces;
using NameSearch.App.Factories;
using NameSearch.App.Helpers;
using NameSearch.App.Helpers.Interfaces;
using NameSearch.App.Services;
using NameSearch.Context;
using NameSearch.Extensions;
using NameSearch.Repository;
using NameSearch.Repository.Interfaces;
using NameSearch.Utility;
using NameSearch.Utility.Interfaces;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
using StructureMap;
using System;
using System.IO;

namespace NameSearch.App
{
    /// <summary>
    /// Application Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The search results directory
        /// </summary>
        public static string SearchResultsDirectory;

        /// <summary>
        /// The export directory
        /// </summary>
        public static string ExportDirectory;

        /// <summary>
        /// Main application entry point.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static int Main(string[] args)
        {
            #region Test args for debugging

            //args = new string[1] { "exportpeople" };
            //args = new string[2] { "importsearches", @"C:\Users\dunca\Desktop\RaleighSwahiliNamesSearch\export.csv" };
            //args = new string[7] { "search", "", "NC", "", @"C:\Users\dunca\Desktop\RaleighSwahiliNamesSearch\SwahiliNames.A.csv", "100" };
            //args = new string[2] { "processresults" "false" };

            #endregion

            #region Configure Logging

            var logsFolder = Path.Combine(Environment.CurrentDirectory, "logs");
            Directory.CreateDirectory(logsFolder);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.RollingFile("logs/namesearch-{Date}.log")
                .WriteTo.Console()
                .CreateLogger();

            #endregion Configure Logging

            var log = Log.Logger;

            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                var configuration = builder.Build();

                #region Create Directories

                ExportDirectory = Path.Combine(Environment.CurrentDirectory, "Exports");
                Directory.CreateDirectory(ExportDirectory);

                SearchResultsDirectory = Path.Combine(Environment.CurrentDirectory, "SearchResults");
                Directory.CreateDirectory(SearchResultsDirectory);

                #endregion

                #region Dependency Injection Container

                // use Dependency injection in console app https://andrewlock.net/using-dependency-injection-in-a-net-core-console-application/
                // add the framework services
                var services = new ServiceCollection()
                    .AddSingleton<IConfiguration>(configuration)
                    .AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true))
                    .AddDbContext<ApplicationDbContext>(optionsBuilder => optionsBuilder.UseSqlite("Data Source=blog.db"))
                    .AddTransient<IMapper, IMapper>((ctx) =>
                    {
                        return MapperFactory.Get();
                    })
                    .AddTransient<JsonSerializerSettings, JsonSerializerSettings>((ctx) =>
                    {
                        return JsonSerializerSettingsFactory.Get();
                    })
                    .AddTransient<IExport, Export>()
                    .AddTransient<IImport, Import>()
                    .AddTransient<IFindPersonController, FindPersonController>()
                    .AddTransient<IPersonHelper, PersonHelper>()
                    .AddTransient<IPersonSearchRequestHelper, PersonSearchRequestHelper>()
                    .AddTransient<IPersonSearchResultHelper, PersonSearchResultHelper>()
                    .AddTransient<IPeopleSearchService, PeopleSearchService>()
                    .AddScoped<IEntityFrameworkRepository, EntityFrameworkRepository>();

                services.AddMvc();

                // add StructureMap
                var container = new Container();
                container.Configure(config =>
                {
                    // Register stuff in container, using the StructureMap APIs...
                    config.Scan(_ =>
                    {
                        _.AssemblyContainingType(typeof(Program));
                        _.WithDefaultConventions();
                    });
                    // Populate the container using the service collection
                    config.Populate(services);
                });

                var peopleSearchService = container.GetInstance<IPeopleSearchService>();

                log.DebugEvent("Main", "Dependency Injection Container configuration complete");

                #endregion Dependency Injection Container

                var options = CommandLineOptions.Parse(args);

                log.InformationEvent("Main", "Run command {command} with arguments {args}", options?.Command, args);

                if (options?.Command == null)
                {
                    // RootCommand will have printed help
                    return 1;
                }

                var result = options.Command.Run(peopleSearchService);

                return result;
            }
            catch (Exception ex)
            {
                log.FatalEvent(ex, "Main", "Fatal Application Failure with message {message}", ex.Message);
                throw;
            }
            finally
            {
                log.InformationEvent("Main", "Application Ending");
                Log.CloseAndFlush();

                // Allow users to see output.  Prevent console closing on its own
                Console.ReadLine();
            }
        }
    }
}