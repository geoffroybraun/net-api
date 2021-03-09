using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Infrastructure.Database.Contexts;
using GB.NetApi.Infrastructure.Database.DAOs;
using GB.NetApi.Infrastructure.Database.Interfaces;
using System;
using System.Threading.Tasks;

namespace GB.NetApi.Infrastructure.Database.Repositories.Commons
{
    public sealed class CommonWritableRepository : ICommonWritableRepository
    {
        #region Fields

        private readonly ICommonRepository Repository;

        #endregion

        public CommonWritableRepository(ICommonRepository repository) => Repository = repository ?? throw new ArgumentNullException(nameof(repository));

        public async Task<bool> CreateAsync<TEntity, TDao>(TEntity entity) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity
        {
            return await CreateAsync<TEntity, TDao>(entity, 1).ConfigureAwait(false);
        }

        public async Task<bool> CreateAsync<TEntity, TDao>(TEntity entity, int expectedSavedChangesCount) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity
        {
            using (var context = Repository.InstanciateContext())
            {
                Task<int> function() => CreateAsync<TEntity, TDao>(context, entity);
                var result = await Repository.ExecuteAsync(function).ConfigureAwait(false);

                return result == expectedSavedChangesCount;
            }
        }

        public async Task<bool> DeleteAsync<TEntity, TDao>(int ID) where TEntity : BaseStorableEntity where TDao : BaseWritableDao<TEntity>, new()
        {
            return await DeleteAsync<TEntity, TDao>(ID, 1).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAsync<TEntity, TDao>(int ID, int expectedSavedChangesCount) where TEntity : BaseStorableEntity where TDao : BaseWritableDao<TEntity>, new()
        {
            using (var context = Repository.InstanciateContext())
            {
                Task<int> function() => DeleteAsync<TEntity, TDao>(context, ID);
                var result = await Repository.ExecuteAsync(function).ConfigureAwait(false);

                return result == expectedSavedChangesCount;
            }
        }

        public async Task<bool> UpdateAsync<TEntity, TDao>(TEntity entity) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity
        {
            return await UpdateAsync<TEntity, TDao>(entity, 1).ConfigureAwait(false);
        }

        public async Task<bool> UpdateAsync<TEntity, TDao>(TEntity entity, int expectedSavedChangesCount) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity
        {
            using (var context = Repository.InstanciateContext())
            {
                Task<int> function() => UpdateAsync<TEntity, TDao>(context, entity);
                var result = await Repository.ExecuteAsync(function).ConfigureAwait(false);

                return result == expectedSavedChangesCount;
            }
        }

        #region Private methods

        private async Task<int> CreateAsync<TEntity, TDao>(BaseDbContext context, TEntity entity) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity
        {
            var dao = new TDao();
            dao.Fill(entity);

            var dbSet = Repository.GetDbSet<TDao>(context);
            _ = dbSet.Add(dao);

            return await context.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task<int> DeleteAsync<TEntity, TDao>(BaseDbContext context, int ID) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity
        {
            var dbSet = Repository.GetDbSet<TDao>(context);
            var dao = await dbSet.FindAsync(ID).ConfigureAwait(false);
            _ = dbSet.Remove(dao);

            return await context.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task<int> UpdateAsync<TEntity, TDao>(BaseDbContext context, TEntity entity) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity
        {
            var dao = new TDao();
            dao.Fill(entity);

            var dbSet = Repository.GetDbSet<TDao>(context);
            var entry = await dbSet.FindAsync(entity.ID).ConfigureAwait(false);
            context.Entry(entry).CurrentValues.SetValues(dao);

            return await context.SaveChangesAsync().ConfigureAwait(false);
        }

        #endregion
    }
}
