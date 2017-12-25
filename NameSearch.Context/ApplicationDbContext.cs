using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NameSearch.Models.Entities;
using System;
using NameSearch.Models.Entities.Abstracts;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace NameSearch.Context
{
    /// <summary>
    /// 
    /// </summary>
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString);
        }

        //Declare public Entities
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Name> Names { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<SearchJob> SearchJobs { get; set; }
        public DbSet<SearchTransaction> SearchTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().HasQueryFilter(p => p.IsActive);
            modelBuilder.Entity<Name>().HasQueryFilter(p => p.IsActive);
            modelBuilder.Entity<Person>().HasQueryFilter(p => p.IsActive);
            modelBuilder.Entity<SearchJob>().HasQueryFilter(p => p.IsActive);
            modelBuilder.Entity<SearchTransaction>().HasQueryFilter(p => p.IsActive);
        }

        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// Soft Deletes and Record Level Auditing.
        /// See also https://www.meziantou.net/2017/07/10/entity-framework-core-soft-delete-using-query-filters
        /// </summary>
        private void OnBeforeSaving()
        {
            var now = DateTime.Now;

            foreach (var entry in ChangeTracker.Entries())
            {
                var entity = entry.Entity as AuditableEntityBase;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreatedDateTime = now;
                        entity.IsActive = true;
                        break;

                    case EntityState.Modified:
                        entity.ModifiedDateTime = now;
                        entity.IsActive = true;
                        break;

                    case EntityState.Deleted:
                        entity.ModifiedDateTime = now;
                        entity.IsActive = false;
                        //Modify instead of deleting
                        entry.State = EntityState.Modified;
                        break;
                }
            }
        }
    }
}
