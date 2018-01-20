using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NameSearch.Context;
using NameSearch.Repository;
using StructureMap;
using Serilog;
using Serilog.Events;

namespace NameSearch.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                //ToDo: Parse args for parameters                

                #region Dependency Injection Container

                // use Dependency injection in console app https://andrewlock.net/using-dependency-injection-in-a-net-core-console-application/
                // add the framework services
                var services = new ServiceCollection()
                    .AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true))
                    //.AddSingleton<DbContext, ApplicationDbContext>()
                    .AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=blog.db"))
                    .AddSingleton<IEntityFrameworkRepository, EntityFrameworkRepository>();

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

                #endregion

                var serviceProvider = container.GetInstance<IServiceProvider>();
                var dbContext = serviceProvider.GetService<DbContext>();
                var repository = serviceProvider.GetService<IEntityFrameworkRepository>();

                //ToDo: Test Directory exists and has access

                //ToDo: Create User Interface with command line
                Console.WriteLine("Hello World!");

                //ToDo: Option to Import Names using SearchNameImporter

                //ToDo: Parse city, state, zip from console input
                string city;
                string state;
                string zip;

                //ToDo: Get List of searchNames
                //var nameImport = Repository.GetFirst<NameImport>(null, o => o.OrderByDescending(y => y.Id));
                //var names = Repository.Get<Name>(x => x.NameImportId == nameImport.Id, o => o.OrderByDescending(y => y.Value));
                //var people = names.Select(x => new NameSearch.Models.Domain.Api.Request.Person
                //{
                //    Name = x.Value,
                //    City = city,
                //    State = state,
                //    Zip = zip
                //}).ToList();

                //ToDo: Execute PeopleFinder.Run() in batches of x (from config file) records.

                //ToDo: Execute PeopleFinder.Run()
                //ToDo: Execute PeopleSearchResultProcessor.Run()
                //ToDo: Execute Export.ToCsv();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Fatal Application Failure");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
