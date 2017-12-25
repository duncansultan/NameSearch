using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using NameSearch.Context;
using NameSearch.Repository;
using StructureMap;

namespace NameSearch.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Dependency Injection Container

            // use Dependency injection in console app https://andrewlock.net/using-dependency-injection-in-a-net-core-console-application/
            // add the framework services
            var services = new ServiceCollection()
                .AddSingleton<DbContext, ApplicationDbContext>()
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

            var serviceProvider = container.GetInstance<IServiceProvider>();

            #endregion

            var dbContext = serviceProvider.GetService<DbContext>();
            var repository = serviceProvider.GetService<IEntityFrameworkRepository>();


            Console.WriteLine("Hello World!");
        }
    }
}
