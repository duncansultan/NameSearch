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
using NameSearch.Extensions;
using System.IO;
using AutoMapper;
using System.Security.AccessControl;
using NameSearch.App.Tasks;
using NameSearch.Api.Controllers.Interfaces;
using NameSearch.Api.Controllers;
using Microsoft.Extensions.Configuration;
using NameSearch.Utility.Interfaces;

namespace NameSearch.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //ToDo: Add sinks and Enrichers
            #region Configure Logging

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            #endregion

            var log = Log.Logger;

            try
            {
                //ToDo: Parse args for parameters
                #region Parse args

                var directory = "";

                var command = ""; //ToDo Create Enum for Search, Import, Export

                //ToDo: Option to Import Names using SearchNameImporter

                //ToDo: Parse city, state, zip from console input

                #endregion

                #region Parse Console Input

                #endregion

                #region Configure Dependencies

                var mapperConfiguration = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Models.Domain.Api.Response.Person, Models.Entities.Person>();
                });
                var mapper = new Mapper(mapperConfiguration);

                #endregion

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

                #region Validate Configuration

                var directoryExists = Directory.Exists(directory);
                if (!directoryExists)
                {
                    log.ErrorEvent("Main", "Directory does not exist {path}", directory);
                    throw new DirectoryNotFoundException(nameof(directory));
                }

                //ToDo: Test Directory exists and has access
                //There is not an easy way to check folder permissions in .net core
                var directoryInfo = new DirectoryInfo(directory);
                var directorySecurity = directoryInfo.GetAccessControl(AccessControlSections.All);

                var directoryHasAccess = true;
                if (!directoryHasAccess)
                {
                    log.ErrorEvent("Main", "Directory access is denied {path}", directoryInfo.FullName);
                    throw new ArgumentException(nameof(directory));
                }

                #endregion

                #region Commands

                var serviceProvider = container.GetInstance<IServiceProvider>();
                var configuration = serviceProvider.GetService<IConfiguration>();
                var repository = serviceProvider.GetService<IEntityFrameworkRepository>();
                var export = serviceProvider.GetService<IExport>();

                //ISearchNameImporter searchNameImporter = new SearchNameImporter();
                //ISearchName searchName = new SearchName();
                //IFindPersonController findPersonController = new FindPersonController(configuration);
                //IPeopleFinder peopleFinder = new PeopleFinder(repository, findPersonController, export);
                //IPeopleSearchResultProcessor peopleSearchResultProcessor = new PeopleSearchResultProcessor(repository, mapper);

                #endregion

                #region Execute Commands

                //ToDo: Execute PeopleFinder.Run() in batches of x (from config file) records.
                //ToDo: Execute PeopleFinder.Run()
                //ToDo: Execute PeopleSearchResultProcessor.Run()
                //ToDo: Execute Export.ToCsv();

                //ToDo: Create User Interface with command line
                Console.WriteLine("Hello World!");

                #endregion
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
