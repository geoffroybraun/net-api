using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Interfaces.Entities;
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
    public interface ICommonReadableRepository
    {
        /// <summary>
        /// Indicates if at least one DAO matches the provided function
        /// </summary>
        /// <typeparam name="TDao">The DAO to query</typeparam>
        /// <param name="model">The <see cref="AnyModel{TDao}"/> model to use when querying</param>
        /// <returns>True if at least one DAO matches the provided function, otherwise false</returns>
        Task<bool> AnyAsync<TDao>(AnyModel<TDao> model) where TDao : BaseDao;

        /// <summary>
        /// Indicates if at least one DAO matches the provided function
        /// </summary>
        /// <typeparam name="TDao">The DAO to query</typeparam>
        /// <param name="model">The <see cref="AnyModel{TDao}"/> model to use when querying</param>
        /// <param name="tracking">The tracking mode to use when querying</param>
        /// <returns>True if at least one DAO matches the provided function, otherwise false</returns>
        Task<bool> AnyAsync<TDao>(AnyModel<TDao> model, ETracking tracking) where TDao : BaseDao;

        /// <summary>
        /// Retrieve an entity using the provided matching function
        /// </summary>
        /// <typeparam name="TDao">The DAO type to query</typeparam>
        /// <typeparam name="TEntity">The entity to retrieve</typeparam>
        /// <param name="model">The <see cref="SingleModel{TDao}"/> model to use when querying</param>
        /// <returns>The found entity</returns>
        Task<TEntity> SingleAsync<TDao, TEntity>(SingleModel<TDao> model) where TDao : BaseDao, ITransformable<TEntity> where TEntity : BaseStorableEntity;

        /// <summary>
        /// Retrieve an entity using the provided matching function
        /// </summary>
        /// <typeparam name="TDao">The DAO type to query</typeparam>
        /// <typeparam name="TEntity">The entity to retrieve</typeparam>
        /// <param name="model">The <see cref="SingleModel{TDao}"/> model to use when querying</param>
        /// <param name="tracking">The tracking mode to use when querying</param>
        /// <returns>The found entity</returns>
        Task<TEntity> SingleAsync<TDao, TEntity>(SingleModel<TDao> model, ETracking tracking) where TDao : BaseDao, ITransformable<TEntity> where TEntity : BaseStorableEntity;

        /// <summary>
        /// Retrieve all stored entities
        /// </summary>
        /// <typeparam name="TDao">The DAO type to query</typeparam>
        /// <typeparam name="TEntity">The entity type to retrieve</typeparam>
        /// <returns>All stored entities</returns>
        Task<IEnumerable<TEntity>> ToListAsync<TDao, TEntity>() where TDao : BaseDao, ITransformable<TEntity> where TEntity : BaseStorableEntity;

        /// <summary>
        /// Retrieve all stored entities
        /// </summary>
        /// <typeparam name="TDao">The DAO type to query</typeparam>
        /// <typeparam name="TEntity">The entity type to retrieve</typeparam>
        /// <param name="tracking">The tracking mode to use when querying</param>
        /// <returns>All stored entities</returns>
        Task<IEnumerable<TEntity>> ToListAsync<TDao, TEntity>(ETracking tracking) where TDao : BaseDao, ITransformable<TEntity> where TEntity : BaseStorableEntity;

        /// <summary>
        /// Retrieve all entities while filtering using multiple functions
        /// </summary>
        /// <typeparam name="TDao">The DAO type to query</typeparam>
        /// <typeparam name="TEntity">The entity type to retrieve</typeparam>
        /// <param name="model">The <see cref="WhereManyModel{TDao}"/> model to use when filtering</param>
        /// <returns>All filtered entities</returns>
        Task<IEnumerable<TEntity>> ToListAsync<TDao, TEntity>(WhereManyModel<TDao> model) where TDao : BaseDao, ITransformable<TEntity> where TEntity : BaseStorableEntity;

        /// <summary>
        /// Retrieve all entities while filtering using multiple functions
        /// </summary>
        /// <typeparam name="TDao">The DAO type to query</typeparam>
        /// <typeparam name="TEntity">The entity type to retrieve</typeparam>
        /// <param name="model">The <see cref="WhereManyModel{TDao}"/> model to use when filtering</param>
        /// <param name="tracking">The tracking mode to use when querying</param>
        /// <returns>All filtered entities</returns>
        Task<IEnumerable<TEntity>> ToListAsync<TDao, TEntity>(WhereManyModel<TDao> model, ETracking tracking) where TDao : BaseDao, ITransformable<TEntity> where TEntity : BaseStorableEntity;
    }
}
