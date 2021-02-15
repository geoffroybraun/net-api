using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Interfaces.Libraries;
using GB.NetApi.Infrastructure.Database.Contexts;
using GB.NetApi.Infrastructure.Database.DAOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GB.NetApi.Infrastructure.Database.Repositories
{
    /// <summary>
    /// Represents an abstract repository which provides useful 'write' methods to deriving classes
    /// </summary>
    /// <typeparam name="TDao">The DAO type to query</typeparam>
    /// <typeparam name="TEntity">The tntity type the DAO can be transformed to</typeparam>
    public abstract class BaseWritableRepository<TDao, TEntity> : BaseReadableRepository<TDao, TEntity> where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity
    {
        protected BaseWritableRepository(Func<BaseDbContext> contextFunction, ITaskHandler taskHandler) : base(contextFunction, taskHandler) { }

        /// <summary>
        /// Add the provided <see cref="TEntity"/> entity
        /// </summary>
        /// <param name="entity">The <see cref="TEntity"/> entity to add</param>
        /// <returns>True if the provided <see cref="TEntity"/> has been added, otherwise false</returns>
        protected async Task<bool> CreateAsync(TEntity entity) => await CreateAsync(entity, 1).ConfigureAwait(false);

        /// <summary>
        /// Add the provided <see cref="TEntity"/> entity while counting all saved changes
        /// </summary>
        /// <param name="entity">The <see cref="TEntity"/> entity to add</param>
        /// <param name="expectedSaveChangesCount">The expected saved changes count when creating the <see cref="TEntity"/> entity</param>
        /// <returns>True if all expected changes have been saved, otherwise false</returns>
        protected async Task<bool> CreateAsync(TEntity entity, int expectedSaveChangesCount)
        {
            using (var context = ContextFunction())
            {
                Task<int> function() => AddAsync(context, entity);
                var result = await TaskHandler.HandleAsync(function).ConfigureAwait(false);

                return result == expectedSaveChangesCount;
            }
        }

        protected async Task<bool> UpdateAsync(TEntity entity) => await UpdateAsync(entity, 1).ConfigureAwait(false);

        protected async Task<bool> UpdateAsync(TEntity entity, int expectedSavedChangesCount)
        {
            using (var context = ContextFunction())
            {
                Task<int> function() => UpdateAsync(context, entity);
                var result = await TaskHandler.HandleAsync(function).ConfigureAwait(false);

                return result == expectedSavedChangesCount;
            }
        }

        #region Private methods

        private async Task<int> AddAsync(BaseDbContext context, TEntity entity)
        {
            var dao = new TDao();
            dao.Fill(entity);

            var dbSet = GetDbSet(context);
            _ = dbSet.Add(dao);

            return await context.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task<int> UpdateAsync(BaseDbContext context, TEntity entity)
        {
            var dao = new TDao();
            dao.Fill(entity);

            var dbSet = GetDbSet(context);
            var entry = await dbSet.FindAsync(entity.ID).ConfigureAwait(false);
            context.Entry(entry).CurrentValues.SetValues(dao);

            return await context.SaveChangesAsync().ConfigureAwait(false);
        }

        #endregion
    }
}
