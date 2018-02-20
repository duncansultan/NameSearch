using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NameSearch.Models.Entities;
using System;
using NameSearch.Models.Entities.Abstracts;
using System.Threading.Tasks;
using System.Threading;

namespace NameSearch.Context
{
    /// <summary>
    /// Application Entity Framework Database Context
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        public ApplicationDbContext()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public ApplicationDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// <para>
        /// Override this method to configure the database (and other options) to be used for this context.
        /// This method is called for each instance of the context that is created.
        /// </para>
        /// <para>
        /// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
        /// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
        /// the options have already been set, and skip some or all of the logic in
        /// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
        /// </para>
        /// </summary>
        /// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
        /// typically define extension methods on this object that allow you to configure the context.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString);
        }

        //Declare public Entities
        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        /// <value>
        /// The addresses.
        /// </value>
        public DbSet<Address> Addresses { get; set; }
        /// <summary>
        /// Gets or sets the associates.
        /// </summary>
        /// <value>
        /// The associates.
        /// </value>
        public DbSet<Associate> Associates { get; set; }
        /// <summary>
        /// Gets or sets the people.
        /// </summary>
        /// <value>
        /// The people.
        /// </value>
        public DbSet<Person> People { get; set; }
        /// <summary>
        /// Gets or sets the phones.
        /// </summary>
        /// <value>
        /// The phones.
        /// </value>
        public DbSet<Phone> Phones { get; set; }
        /// <summary>
        /// Gets or sets the person search results.
        /// </summary>
        /// <value>
        /// The person search results.
        /// </value>
        public DbSet<PersonSearch> PersonSearches { get; set; }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Filter Inactive Records
            modelBuilder.Entity<Address>().HasQueryFilter(p => p.IsActive);
            modelBuilder.Entity<Associate>().HasQueryFilter(p => p.IsActive);
            modelBuilder.Entity<Person>().HasQueryFilter(p => p.IsActive);
            modelBuilder.Entity<PersonSearch>().HasQueryFilter(p => p.IsActive);
            modelBuilder.Entity<Phone>().HasQueryFilter(p => p.IsActive);

            //Cascading deletes will not work with soft deletes.  The cascade only happens as the delete command is issued to the database.
            //.OnDelete(DeleteBehavior.Cascade)

            //modelBuilder.Entity<Person>()
            //    .HasMany(a => a.Addresses)
            //    .WithOne(p => p.Person)
            //    .IsRequired(false);

            //modelBuilder.Entity<Person>()
            //    .HasMany(a => a.Phones)
            //    .WithOne(p => p.Person)
            //    .IsRequired(false);

            //modelBuilder.Entity<Person>()
            //    .HasMany(a => a.Associates)
            //    .WithOne(p => p.Person)
            //    .IsRequired(false);

            //modelBuilder.Entity<Person>()
            //    .HasOne(a => a.PersonSearchResult)
            //    .WithOne(p => p.Person)
            //    .IsRequired(false);

            //modelBuilder.Entity<PersonSearchJob>()
            //    .HasMany(a => a.PersonSearchResults)
            //    .WithOne(p => p.PersonSearchJob)
            //    .IsRequired(true);
        }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <returns>
        /// The number of state entries written to the database.
        /// </returns>
        /// <remarks>
        /// This method will automatically call <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
        /// changes to entity instances before saving to the underlying database. This can be disabled via
        /// <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
        /// </remarks>
        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indicates whether <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges" /> is called after the changes have
        /// been sent successfully to the database.</param>
        /// <returns>
        /// The number of state entries written to the database.
        /// </returns>
        /// <remarks>
        /// This method will automatically call <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
        /// changes to entity instances before saving to the underlying database. This can be disabled via
        /// <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
        /// </remarks>
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// Asynchronously saves all changes made in this context to the database.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous save operation. The task result contains the
        /// number of state entries written to the database.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method will automatically call <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
        /// changes to entity instances before saving to the underlying database. This can be disabled via
        /// <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
        /// </para>
        /// <para>
        /// Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        /// that any asynchronous operations have completed before calling another method on this context.
        /// </para>
        /// </remarks>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Asynchronously saves all changes made in this context to the database.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indicates whether <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges" /> is called after the changes have
        /// been sent successfully to the database.</param>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous save operation. The task result contains the
        /// number of state entries written to the database.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method will automatically call <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
        /// changes to entity instances before saving to the underlying database. This can be disabled via
        /// <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
        /// </para>
        /// <para>
        /// Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        /// that any asynchronous operations have completed before calling another method on this context.
        /// </para>
        /// </remarks>
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
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
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
