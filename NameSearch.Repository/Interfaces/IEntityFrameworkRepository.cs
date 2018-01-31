using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NameSearch.Context;
using NameSearch.Models.Entities.Interfaces;

namespace NameSearch.Repository.Interfaces
{
    /// <summary>
    /// Interface for Entity Framework Repository
    /// </summary>
    public interface IEntityFrameworkRepository
    {
        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        ApplicationDbContext Context { get; set; }

        /// <summary>
        ///     Get All Entities by Filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns>An enumerable of the entity.</returns>
        IEnumerable<TEntity> GetAll<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity<TEntity>;

        /// <summary>
        ///     Get All Entities by Filter asynchronous
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns>An async task of an enumerable of the entity.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity<TEntity>;

        /// <summary>
        ///     Get a single Entity by Filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns>An enumerable of the entity for the given filter.</returns>
        IEnumerable<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity<TEntity>;

        /// <summary>
        ///     Get Entities by Filter asynchronous
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns>An async task of an enumerable of the entity for the given filter.</returns>
        Task<IEnumerable<TEntity>> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity<TEntity>;

        /// <summary>
        ///     Get a single Entity by Filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="includeProperties"></param>
        /// <returns>A single entity for the given filter.</returns>
        TEntity GetOne<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null)
            where TEntity : class, IEntity<TEntity>;

        /// <summary>
        ///     Get a single Entity by Filter asynchronous
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="includeProperties"></param>
        /// <returns>An async task of a single entity for the given filter.</returns>
        Task<TEntity> GetOneAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null)
            where TEntity : class, IEntity<TEntity>;

        /// <summary>
        ///     Get the first Entity in the order list by Filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns>The first entity for the given filter.</returns>
        TEntity GetFirst<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null)
            where TEntity : class, IEntity<TEntity>;

        /// <summary>
        ///     Get the first Entity in the order list by Filter asynchronous
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns>An async task of the first entity for the given filter.</returns>
        Task<TEntity> GetFirstAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null)
            where TEntity : class, IEntity<TEntity>;

        /// <summary>
        ///     Get an Entity by Id
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns>The entity for the given id.</returns>
        TEntity GetById<TEntity>(object id)
            where TEntity : class, IEntity<TEntity>;

        /// <summary>
        ///     Get an Entity by Id asynchronous
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns>An async task of the entity for the given id.</returns>
        Task<TEntity> GetByIdAsync<TEntity>(object id)
            where TEntity : class, IEntity<TEntity>;

        /// <summary>
        ///     Get a count of Entities by filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns>A count of entitites.</returns>
        int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity<TEntity>;

        /// <summary>
        ///     Get a count of Entities by filter asynchronous
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns>An async task of a count of entitites.</returns>
        Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity<TEntity>;

        /// <summary>
        ///     Check if an Entity exists by filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns><c>true</c> if the entity could be found; otherwise, <c>false</c>.</returns>
        bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity<TEntity>;

        /// <summary>
        ///     Check if an Entity exists by filter asynchronous
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns>An async task for <c>true</c> if the entity could be found; otherwise, <c>false</c>.</returns>
        Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity<TEntity>;

        /// <summary>
        ///     Persist New Entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        object Create<TEntity>(TEntity entity)
            where TEntity : class, IEntity<TEntity>;

        /// <summary>
        ///     Update Existing Entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        void Update<TEntity>(TEntity entity)
            where TEntity : class, IEntity<TEntity>;

        /// <summary>
        ///     Delete Existing Entity by Id
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        void Delete<TEntity>(object id)
            where TEntity : class, IEntity<TEntity>;

        /// <summary>
        ///     Delete Existing Entity by Entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity<TEntity>;

        /// <summary>
        ///     Persist In-Memory Changes
        /// </summary>
        void Save();

        /// <summary>
        ///     Persist In-Memory Changes asynchronous
        /// </summary>
        /// <returns>A task</returns>
        Task SaveAsync();
    }
}
