using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Interfaces.Libraries;
using GB.NetApi.Domain.Services.Extensions;
using GB.NetApi.Infrastructure.Database.Contexts;
using GB.NetApi.Infrastructure.Database.DAOs;
using GB.NetApi.Infrastructure.Database.Enums;
using GB.NetApi.Infrastructure.Database.Extensions;
using GB.NetApi.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GB.NetApi.Infrastructure.Database.Repositories
{
    /// <summary>
    /// Represents an abstract repository which provides useful 'read-only' methods to deriving classes
    /// </summary>
    /// <typeparam name="TDao">The DAO type to query</typeparam>
    /// <typeparam name="TEntity">The tntity type the DAO can be transformed to</typeparam>
    public abstract class BaseReadableRepository<TDao, TEntity> where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity
    {
        #region Properties

        protected readonly Func<BaseDbContext> ContextFunction;
        protected readonly ITaskHandler TaskHandler;

        #endregion

        protected BaseReadableRepository(Func<BaseDbContext> contextFunction, ITaskHandler taskHandler)
        {
            ContextFunction = contextFunction ?? throw new ArgumentNullException(nameof(contextFunction));
            TaskHandler = taskHandler ?? throw new ArgumentNullException(nameof(taskHandler));
        }

        /// <summary>
        /// Indicates if any <see cref="TDao"/> matches the provided model function
        /// </summary>
        /// <param name="model">The model to use when looking for matching <see cref="TDao"/></param>
        /// <returns>True if at least one <see cref="TDao"/> matches the provided model function, otherwise false</returns>
        protected async Task<bool> AnyAsync(AnyModel<TDao> model) => await AnyAsync(model, ETracking.Disabled).ConfigureAwait(false);

        /// <summary>
        /// Indicates if any <see cref="TDao"/> matches the provided model function
        /// </summary>
        /// <param name="model">The model to use when looking for matching <see cref="TDao"/></param>
        /// <param name="tracking">The tracking mode to use when querying</param>
        /// <returns>True if at least one <see cref="TDao"/> matches the provided model function, otherwise false</returns>
        protected async Task<bool> AnyAsync(AnyModel<TDao> model, ETracking tracking)
        {
            using (var context = ContextFunction())
            {
                Task<bool> function() => GetQuery(context, tracking)
                    .AnyAsync(model.Any);

                return await TaskHandler.HandleAsync(function)
                    .ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Returns the related <see cref="DbSet{TDao}"/> property from the provided <see cref="BaseDbContext"/> implementation based on the required <see cref="ETracking"/> mode
        /// </summary>
        /// <param name="context">The <see cref="BaseDbContext"/> implementation where to find a <see cref="DbSet{TDao}"/> property</param>
        /// <param name="tracking">The tracking mode to use when querying</param>
        /// <returns>The found <see cref="DbSet{TDao}"/> property</returns>
        protected IQueryable<TDao> GetQuery(BaseDbContext context, ETracking tracking)
        {
            var dbSet = GetDbSet(context);

            return tracking == ETracking.Disabled ? dbSet.AsNoTracking() : dbSet;
        }

        /// <summary>
        /// Returns the related <see cref="DbSet{TDao}"/> property from the provided <see cref="BaseDbContext"/> implementation
        /// </summary>
        /// <param name="context">The <see cref="BaseDbContext"/> implementation where to find a <see cref="DbSet{TDao}"/> property</param>
        /// <returns>The found <see cref="DbSet{TDao}"/> property</returns>
        protected DbSet<TDao> GetDbSet(BaseDbContext context)
        {
            var properties = typeof(BaseDbContext).GetProperties();
            var dbSetProperty = properties.SingleOrDefault(p => p.PropertyType == typeof(DbSet<TDao>));

            if (dbSetProperty is null)
                throw new ArgumentException($"No property of '{typeof(DbSet<TDao>).Name}' has been found in context '{context.GetType().Name}'...");

            return dbSetProperty.GetValue(context) as DbSet<TDao>;
        }

        /// <summary>
        /// Retrieve a single <see cref="TEntity"/> entity based ont the provided function
        /// </summary>
        /// <param name="model">The <see cref="SingleModel{TDao}"/> to use when querying</param>
        /// <returns>The queried <see cref="TEntity"/> entity</returns>
        protected async Task<TEntity> SingleAsync(SingleModel<TDao> model) => await SingleAsync(model, ETracking.Disabled).ConfigureAwait(false);

        /// <summary>
        /// Retrieve a single <see cref="TEntity"/> entity based ont the provided function
        /// </summary>
        /// <param name="model">The <see cref="SingleModel{TDao}"/> to use when querying</param>
        /// <param name="tracking">The tracking mode to use when querying</param>
        /// <returns>The queried <see cref="TEntity"/> entity</returns>
        protected async Task<TEntity> SingleAsync(SingleModel<TDao> model, ETracking tracking)
        {
            using (var context = ContextFunction())
            {
                Task<TDao> function() => GetQuery(context, tracking).SingleOrDefaultAsync(model.SingleOrDefault);
                var result = await TaskHandler.HandleAsync(function).ConfigureAwait(false);

                return Transform(result);
            }
        }

        /// <summary>
        /// Retrieve all stored <see cref="TEntity"/> entities
        /// </summary>
        /// <param name="tracking">The tracking mode to use when querying</param>
        /// <returns>All stored <see cref="TEntity"/> entities</returns>
        protected async Task<IEnumerable<TEntity>> ToListAsync() => await ToListAsync(ETracking.Disabled).ConfigureAwait(false);

        /// <summary>
        /// Retrieve all stored <see cref="TEntity"/> entities
        /// </summary>
        /// <param name="tracking">The tracking mode to use when querying</param>
        /// <returns>All stored <see cref="TEntity"/> entities</returns>
        protected async Task<IEnumerable<TEntity>> ToListAsync(ETracking tracking)
        {
            using (var context = ContextFunction())
            {
                Task<List<TDao>> function() => GetQuery(context, tracking)
                    .ToListAsync();

                var result = await TaskHandler.HandleAsync(function)
                    .ConfigureAwait(false);

                return Transform(result);
            }
        }

        /// <summary>
        /// Retrieves all filtered <see cref="TEntity"/> entities
        /// </summary>
        /// <param name="model">The <see cref="WhereManyModel{TDao}"/> to use when filtering</param>
        /// <returns>All filtered <see cref="TEntity"/> entities</returns>
        protected async Task<IEnumerable<TEntity>> ToListAsync(WhereManyModel<TDao> model) => await ToListAsync(model, ETracking.Disabled).ConfigureAwait(false);

        /// <summary>
        /// Retrieves all filtered <see cref="TEntity"/> entities
        /// </summary>
        /// <param name="model">The <see cref="WhereManyModel{TDao}"/> to use when filtering</param>
        /// <param name="tracking">The tracking mode to use when querying</param>
        /// <returns>All filtered <see cref="TEntity"/> entities</returns>
        protected async Task<IEnumerable<TEntity>> ToListAsync(WhereManyModel<TDao> model, ETracking tracking)
        {
            using (var context = ContextFunction())
            {
                Task<List<TDao>> function() => GetQuery(context, tracking)
                    .WhereMany(model.WhereMany)
                    .ToListAsync();
                var result = await TaskHandler.HandleAsync(function)
                    .ConfigureAwait(false);

                return Transform(result);
            }
        }

        #region Private methods

        private static IEnumerable<TEntity> Transform(IEnumerable<TDao> daos) => daos.IsNotNullNorEmpty() ? daos.Select(Transform) : default;

        private static TEntity Transform(TDao dao) => dao is not null ? dao.Transform() : default;

        #endregion
    }
}
