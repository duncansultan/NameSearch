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

            Console.WriteLine("Hello World!");
        }
    }
}
