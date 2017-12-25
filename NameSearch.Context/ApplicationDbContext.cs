using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NameSearch.Models.Entities;

namespace NameSearch.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string connectionString;

        public ApplicationDbContext()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public ApplicationDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Name> Names { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<SearchJob> SearchJobs { get; set; }
        public DbSet<SearchTransaction> SearchTransactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
