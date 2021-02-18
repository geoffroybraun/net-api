using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Infrastructure.Database.DAOs;
using GB.NetApi.Infrastructure.Database.Enums;
using GB.NetApi.Infrastructure.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GB.NetApi.Infrastructure.Database.Interfaces
{
    /// <summary>
    /// Represents a common repository which provides 'read-only' methods
    /// </summary>
    public interface ICommonReadableRepository : ICommonRepository
    {
        /// <summary>
        /// Indicates if any <see cref="TDao"/> matches the provided model function
        /// </summary>
        /// <param name="model">The model to use when looking for matching <see cref="TDao"/></param>
        /// <returns>True if at least one <see cref="TDao"/> matches the provided model function, otherwise false</returns>
        Task<bool> AnyAsync<TDao, TEntity>(AnyModel<TDao> model) where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity;

        /// <summary>
        /// Indicates if any <see cref="TDao"/> matches the provided model function
        /// </summary>
        /// <param name="model">The model to use when looking for matching <see cref="TDao"/></param>
        /// <param name="tracking">The tracking mode to use when querying</param>
        /// <returns>True if at least one <see cref="TDao"/> matches the provided model function, otherwise false</returns>
        Task<bool> AnyAsync<TDao, TEntity>(AnyModel<TDao> model, ETracking tracking) where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity;

        /// <summary>
        /// Retrieve a single <see cref="TEntity"/> entity based ont the provided function
        /// </summary>
        /// <param name="model">The <see cref="SingleModel{TDao}"/> to use when querying</param>
        /// <returns>The queried <see cref="TEntity"/> entity</returns>
        Task<TEntity> SingleAsync<TDao, TEntity>(SingleModel<TDao> model) where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity;

        /// <summary>
        /// Retrieve a single <see cref="TEntity"/> entity based ont the provided function
        /// </summary>
        /// <param name="model">The <see cref="SingleModel{TDao}"/> to use when querying</param>
        /// <param name="tracking">The tracking mode to use when querying</param>
        /// <returns>The queried <see cref="TEntity"/> entity</returns>
        Task<TEntity> SingleAsync<TDao, TEntity>(SingleModel<TDao> model, ETracking tracking) where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity;

        /// <summary>
        /// Retrieve all stored <see cref="TEntity"/> entities
        /// </summary>
        /// <param name="tracking">The tracking mode to use when querying</param>
        /// <returns>All stored <see cref="TEntity"/> entities</returns>
        Task<IEnumerable<TEntity>> ToListAsync<TDao, TEntity>() where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity;

        /// <summary>
        /// Retrieve all stored <see cref="TEntity"/> entities
        /// </summary>
        /// <param name="tracking">The tracking mode to use when querying</param>
        /// <returns>All stored <see cref="TEntity"/> entities</returns>
        Task<IEnumerable<TEntity>> ToListAsync<TDao, TEntity>(ETracking tracking) where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity;

        /// <summary>
        /// Retrieves all filtered <see cref="TEntity"/> entities
        /// </summary>
        /// <param name="model">The <see cref="WhereManyModel{TDao}"/> to use when filtering</param>
        /// <returns>All filtered <see cref="TEntity"/> entities</returns>
        Task<IEnumerable<TEntity>> ToListAsync<TDao, TEntity>(WhereManyModel<TDao> model) where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity;

        /// <summary>
        /// Retrieves all filtered <see cref="TEntity"/> entities
        /// </summary>
        /// <param name="model">The <see cref="WhereManyModel{TDao}"/> to use when filtering</param>
        /// <param name="tracking">The tracking mode to use when querying</param>
        /// <returns>All filtered <see cref="TEntity"/> entities</returns>
        Task<IEnumerable<TEntity>> ToListAsync<TDao, TEntity>(WhereManyModel<TDao> model, ETracking tracking) where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity;
    }
}
