using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NameSearch.Context;
using NameSearch.Extensions;
using NameSearch.Repository;
using NameSearch.Repository.Interfaces;
using Serilog;
using Serilog.Events;
using StructureMap;
using System;

namespace NameSearch.App
{
    /// <summary>
    /// Application Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main application entry point.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static int Main(string[] args)
        {
            #region Configure Logging

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.RollingFile("namesearch-{Date}.log")
                .WriteTo.Console()
                .CreateLogger();

            #endregion Configure Logging

            var log = Log.Logger;

            try
            {
                #region Dependency Injection Container

                // use Dependency injection in console app https://andrewlock.net/using-dependency-injection-in-a-net-core-console-application/
                // add the framework services
                var services = new ServiceCollection()
                    .AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true))
                    .AddDbContext<ApplicationDbContext>(optionsBuilder => optionsBuilder.UseSqlite("Data Source=blog.db"))
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

                #endregion Dependency Injection Container

                var options = CommandLineOptions.Parse(args);

                if (options?.Command == null)
                {
                    // RootCommand will have printed help
                    return 1;
                }

                return options.Command.Run();
            }
            catch (Exception ex)
            {
                log.FatalEvent(ex, "Fatal Application Failure", null);
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}