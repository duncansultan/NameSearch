using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NameSearch.Context;
using NameSearch.Models.Entities.Interfaces;
using Serilog;

// ReSharper disable RedundantTypeArgumentsOfMethod

namespace NameSearch.Repository
{
    /// <inheritdoc cref="IEntityFrameworkRepository" />
    /// <summary>
    /// Entity Framework Repository
    /// </summary>
    /// <seealso cref="T:NetCoreSqlLite.Repository.IEntityFrameworkRepository" />
    /// <seealso cref="T:System.IDisposable" />
    public class EntityFrameworkRepository : IEntityFrameworkRepository, IDisposable
    {
        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        public ApplicationDbContext Context { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFrameworkRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public EntityFrameworkRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Retreive IQueryable by filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns>A queryable object of the entity for the given filter.</returns>
        protected virtual IQueryable<TEntity> GetQueryable<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity<TEntity>
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        /// <inheritdoc />
        public virtual IEnumerable<TEntity> GetAll<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity<TEntity>
        {
            return GetQueryable<TEntity>(null, orderBy, includeProperties, skip, take).ToList();
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = null,
        int? skip = null,
        int? take = null)
        where TEntity : class, IEntity<TEntity>
        {
            return await GetQueryable<TEntity>(null, orderBy, includeProperties, skip, take).ToListAsync();
        }

        /// <inheritdoc />
        public virtual IEnumerable<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity<TEntity>
        {
            return GetQueryable<TEntity>(filter, orderBy, includeProperties, skip, take).ToList();
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TEntity>> GetAsync<TEntity>(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = null,
        int? skip = null,
        int? take = null)
        where TEntity : class, IEntity<TEntity>
        {
            return await GetQueryable<TEntity>(filter, orderBy, includeProperties, skip, take).ToListAsync();
        }

        /// <inheritdoc />
        public virtual TEntity GetOne<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "")
            where TEntity : class, IEntity<TEntity>
        {
            return GetQueryable<TEntity>(filter, null, includeProperties).SingleOrDefault();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> GetOneAsync<TEntity>(
        Expression<Func<TEntity, bool>> filter = null,
        string includeProperties = null)
        where TEntity : class, IEntity<TEntity>
        {
            return await GetQueryable<TEntity>(filter, null, includeProperties).SingleOrDefaultAsync();
        }

        /// <inheritdoc />
        public virtual TEntity GetFirst<TEntity>(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "")
           where TEntity : class, IEntity<TEntity>
        {
            return GetQueryable<TEntity>(filter, orderBy, includeProperties).FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> GetFirstAsync<TEntity>(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = null)
        where TEntity : class, IEntity<TEntity>
        {
            return await GetQueryable<TEntity>(filter, orderBy, includeProperties).FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public virtual TEntity GetById<TEntity>(object id)
            where TEntity : class, IEntity<TEntity>
        {
            return Context.Set<TEntity>().Find(id);
        }

        /// <inheritdoc />
        public virtual Task<TEntity> GetByIdAsync<TEntity>(object id)
        where TEntity : class, IEntity<TEntity>
        {
            return Context.Set<TEntity>().FindAsync(id);
        }

        /// <inheritdoc />
        public virtual int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity<TEntity>
        {
            return GetQueryable<TEntity>(filter).Count();
        }

        /// <inheritdoc />
        public virtual Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
        where TEntity : class, IEntity<TEntity>
        {
            return GetQueryable<TEntity>(filter).CountAsync();
        }

        /// <inheritdoc />
        public virtual bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity<TEntity>
        {
            return GetQueryable<TEntity>(filter).Any();
        }

        /// <inheritdoc />
        public virtual Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
        where TEntity : class, IEntity<TEntity>
        {
            return GetQueryable<TEntity>(filter).AnyAsync();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Context.Dispose();
        }

        /// <inheritdoc />
        public virtual object Create<TEntity>(TEntity entity)
            where TEntity : class, IEntity<TEntity>
        {

            entity.CreatedDateTime = DateTime.UtcNow;
            entity.ModifiedDateTime = DateTime.UtcNow;
            return Context.Set<TEntity>().Add(entity);
        }

        /// <inheritdoc />
        public virtual void Update<TEntity>(TEntity entity)
            where TEntity : class, IEntity<TEntity>
        {
            entity.ModifiedDateTime = DateTime.UtcNow;
            Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        /// <inheritdoc />
        public virtual void Delete<TEntity>(object id)
            where TEntity : class, IEntity<TEntity>
        {
            var entity = Context.Set<TEntity>().Find(id);
            Delete(entity);
        }

        /// <inheritdoc />
        public virtual void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity<TEntity>
        {
            var dbSet = Context.Set<TEntity>();
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        /// <inheritdoc />
        public virtual void Save()
        {
            try
            {
                if (Context.ChangeTracker.HasChanges())
                {

                    bool saveFailed;
                    do
                    {
                        saveFailed = false;

                        try
                        {
                            var result = Context.SaveChanges();
                            Log.Information("Save Result {0}", result);
                        }
                        catch (DbUpdateConcurrencyException ex)
                        {
                            saveFailed = true;
                            Log.Warning("Optimistic Concurrency Fault, reloading entity and trying again.");
                            //TODO: custom optimistic concurrency resolution
                            ex.Entries.Single().Reload();
                        }

                    } while (saveFailed);


                }
                else
                {
                    Log.Warning("No Changes to be saved");
                }

            }
            catch (DbUpdateException e)
            {
                Log.Logger.Fatal(e, "Save failed with DbUpdateException.");
            }
        }

        /// <inheritdoc />
        public virtual Task SaveAsync()
        {
            try
            {
                return Context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                Log.Logger.Fatal(e, "SaveAsync failed with DbUpdateException.");
            }
            var result = Task.FromResult(0);
            Log.Information("SaveAsync Result: {@0}", result);
            return result;
        }
    }
}
