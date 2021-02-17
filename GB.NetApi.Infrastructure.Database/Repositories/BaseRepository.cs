using GB.NetApi.Domain.Models.Interfaces.Libraries;
using GB.NetApi.Infrastructure.Database.Contexts;
using GB.NetApi.Infrastructure.Database.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace GB.NetApi.Infrastructure.Database.Repositories
{
    /// <summary>
    /// Represents an abstract repository which provides both <see cref="BaseDbContext"/> and <see cref="ITaskHandler"/> implementations to deriving classes
    /// </summary>
    public abstract class BaseRepository
    {
        #region Properties

        protected readonly Func<BaseDbContext> ContextFunction;
        protected readonly ITaskHandler TaskHandler;

        #endregion

        protected BaseRepository(Func<BaseDbContext> contextFunction, ITaskHandler taskHandler)
        {
            ContextFunction = contextFunction ?? throw new ArgumentNullException(nameof(contextFunction));
            TaskHandler = taskHandler ?? throw new ArgumentNullException(nameof(taskHandler));
        }

        /// <summary>
        /// Returns the related <see cref="DbSet{TDao}"/> property from the provided <see cref="BaseDbContext"/> implementation based on the required <see cref="ETracking"/> mode
        /// </summary>
        /// <param name="context">The <see cref="BaseDbContext"/> implementation where to find a <see cref="DbSet{TDao}"/> property</param>
        /// <returns>The found <see cref="DbSet{TDao}"/> property</returns>
        protected IQueryable<TDao> GetQuery<TDao>(BaseDbContext context) where TDao : class => GetQuery<TDao>(context, ETracking.Disabled);

        /// <summary>
        /// Returns the related <see cref="DbSet{TDao}"/> property from the provided <see cref="BaseDbContext"/> implementation based on the required <see cref="ETracking"/> mode
        /// </summary>
        /// <param name="context">The <see cref="BaseDbContext"/> implementation where to find a <see cref="DbSet{TDao}"/> property</param>
        /// <param name="tracking">The tracking mode to use when querying</param>
        /// <returns>The found <see cref="DbSet{TDao}"/> property</returns>
        protected IQueryable<TDao> GetQuery<TDao>(BaseDbContext context, ETracking tracking) where TDao : class
        {
            var dbSet = GetDbSet<TDao>(context);

            return tracking == ETracking.Disabled ? dbSet.AsNoTracking() : dbSet;
        }

        /// <summary>
        /// Returns the related <see cref="DbSet{TDao}"/> property from the provided <see cref="BaseDbContext"/> implementation
        /// </summary>
        /// <param name="context">The <see cref="BaseDbContext"/> implementation where to find a <see cref="DbSet{TDao}"/> property</param>
        /// <returns>The found <see cref="DbSet{TDao}"/> property</returns>
        protected DbSet<TDao> GetDbSet<TDao>(BaseDbContext context) where TDao : class
        {
            var properties = typeof(BaseDbContext).GetProperties();
            var dbSetProperty = properties.SingleOrDefault(p => p.PropertyType == typeof(DbSet<TDao>));

            if (dbSetProperty is null)
                throw new ArgumentException($"No property of '{typeof(DbSet<TDao>).Name}' has been found in context '{context.GetType().Name}'...");

            return dbSetProperty.GetValue(context) as DbSet<TDao>;
        }
    }
}
