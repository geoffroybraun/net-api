using GB.NetApi.Infrastructure.Database.Contexts;
using GB.NetApi.Infrastructure.Database.DAOs;
using GB.NetApi.Infrastructure.Database.Interfaces;
using GB.NetApi.Infrastructure.Database.Models;
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

        public async Task<bool> CreateAsync<TDao>(CreateModel<TDao> model) where TDao : BaseDao, new()
        {
            return await CreateAsync(model, 1).ConfigureAwait(false);
        }

        public async Task<bool> CreateAsync<TDao>(CreateModel<TDao> model, int expectedSavedChangesCount) where TDao : BaseDao, new()
        {
            using (var context = Repository.InstanciateContext())
            {
                Task<int> function() => CreateAsync(context, model);
                var result = await Repository.ExecuteAsync(function).ConfigureAwait(false);

                return result == expectedSavedChangesCount;
            }
        }

        public async Task<bool> DeleteAsync<TDao>(DeleteModel<TDao> model) where TDao : BaseDao
        {
            return await DeleteAsync(model, 1).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAsync<TDao>(DeleteModel<TDao> model, int expectedSavedChangesCount) where TDao : BaseDao
        {
            using (var context = Repository.InstanciateContext())
            {
                Task<int> function() => DeleteAsync(context, model);
                var result = await Repository.ExecuteAsync(function).ConfigureAwait(false);

                return result == expectedSavedChangesCount;
            }
        }

        public async Task<bool> UpdateAsync<TDao>(UpdateModel<TDao> model) where TDao : BaseDao
        {
            return await UpdateAsync(model, 1).ConfigureAwait(false);
        }

        public async Task<bool> UpdateAsync<TDao>(UpdateModel<TDao> model, int expectedSavedChangesCount) where TDao : BaseDao
        {
            using (var context = Repository.InstanciateContext())
            {
                Task<int> function() => UpdateAsync(context, model);
                var result = await Repository.ExecuteAsync(function).ConfigureAwait(false);

                return result == expectedSavedChangesCount;
            }
        }

        #region Private methods

        private async Task<int> CreateAsync<TDao>(BaseDbContext context, CreateModel<TDao> model) where TDao : BaseDao, new()
        {
            if (model.HasPreCreate)
                model.ExecutePreCreate(context);

            var dao = new TDao();
            model.SetDaoProperties(dao);
            _ = await Repository.GetDbSet<TDao>(context).AddAsync(dao).ConfigureAwait(false);

            if (model.HasPostCreate)
                model.ExecutePostCreate(context, dao);

            return await context.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task<int> DeleteAsync<TDao>(BaseDbContext context, DeleteModel<TDao> model) where TDao : BaseDao
        {
            if (model.HasPreDelete)
                model.ExecutePreDelete(context);

            var dbSet = Repository.GetDbSet<TDao>(context);
            var dao = await dbSet.FindAsync(model.ID).ConfigureAwait(false);
            _ = dbSet.Remove(dao);

            if (model.HasPostDelete)
                model.ExecutePostDelete(context, dao);

            return await context.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task<int> UpdateAsync<TDao>(BaseDbContext context, UpdateModel<TDao> model) where TDao : BaseDao
        {
            if (model.HasPreUpdate)
                model.ExecutePreUpdate(context);

            var dao = await Repository.GetDbSet<TDao>(context).FindAsync(model.ID).ConfigureAwait(false);
            model.UpdateDaoProperties(dao);

            if (model.HasPostUpdate)
                model.ExecutePostUpdate(context, dao);

            return await context.SaveChangesAsync().ConfigureAwait(false);
        }

        #endregion
    }
}
