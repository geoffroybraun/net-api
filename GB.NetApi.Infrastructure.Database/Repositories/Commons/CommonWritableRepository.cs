using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Infrastructure.Database.Contexts;
using GB.NetApi.Infrastructure.Database.DAOs;
using GB.NetApi.Infrastructure.Database.Enums;
using GB.NetApi.Infrastructure.Database.Interfaces;
using GB.NetApi.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GB.NetApi.Infrastructure.Database.Repositories.Commons
{
    public sealed class CommonWritableRepository : ICommonWritableRepository
    {
        #region Fields

        private readonly ICommonReadableRepository Repository;

        #endregion

        public CommonWritableRepository(ICommonReadableRepository repository) => Repository = repository ?? throw new ArgumentNullException(nameof(repository));

        public async Task<bool> AnyAsync<TDao, TEntity>(AnyModel<TDao> model) where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity
        {
            return await Repository.AnyAsync<TDao, TEntity>(model).ConfigureAwait(false);
        }

        public async Task<bool> AnyAsync<TDao, TEntity>(AnyModel<TDao> model, ETracking tracking) where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity
        {
            return await Repository.AnyAsync<TDao, TEntity>(model, tracking).ConfigureAwait(false);
        }

        public async Task<bool> CreateAsync<TEntity, TDao>(TEntity entity) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity
        {
            return await CreateAsync<TEntity, TDao>(entity, 1).ConfigureAwait(false);
        }

        public async Task<bool> CreateAsync<TEntity, TDao>(TEntity entity, int expectedSaveChangesCount) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity
        {
            using (var context = InstanciateContext())
            {
                Task<int> function() => AddAsync<TEntity, TDao>(context, entity);
                var result = await ExecuteAsync(function).ConfigureAwait(false);

                return result == expectedSaveChangesCount;
            }
        }

        public async Task<bool> DeleteAsync<TEntity, TDao>(int ID) where TEntity : BaseStorableEntity where TDao : BaseWritableDao<TEntity>, new()
        {
            return await DeleteAsync<TEntity, TDao>(ID, 1).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAsync<TEntity, TDao>(int ID, int expectedSavedChangesCount) where TEntity : BaseStorableEntity where TDao : BaseWritableDao<TEntity>, new()
        {
            using (var context = InstanciateContext())
            {
                Task<int> function() => DeleteAsync<TEntity, TDao>(context, ID);
                var result = await ExecuteAsync(function).ConfigureAwait(false);

                return result == expectedSavedChangesCount;
            }
        }

        public async Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> taskFunction) => await Repository.ExecuteAsync(taskFunction).ConfigureAwait(false);

        public DbSet<TDao> GetDbSet<TDao>(BaseDbContext context) where TDao : class => Repository.GetDbSet<TDao>(context);

        public IQueryable<TDao> GetQuery<TDao>(BaseDbContext context) where TDao : class => Repository.GetQuery<TDao>(context);

        public IQueryable<TDao> GetQuery<TDao>(BaseDbContext context, ETracking tracking) where TDao : class => Repository.GetQuery<TDao>(context, tracking);

        public BaseDbContext InstanciateContext() => Repository.InstanciateContext();

        public async Task<TEntity> SingleAsync<TDao, TEntity>(SingleModel<TDao> model) where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity
        {
            return await Repository.SingleAsync<TDao, TEntity>(model).ConfigureAwait(false);
        }

        public async Task<TEntity> SingleAsync<TDao, TEntity>(SingleModel<TDao> model, ETracking tracking) where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity
        {
            return await Repository.SingleAsync<TDao, TEntity>(model, tracking).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> ToListAsync<TDao, TEntity>() where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity
        {
            return await Repository.ToListAsync<TDao, TEntity>().ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> ToListAsync<TDao, TEntity>(ETracking tracking) where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity
        {
            return await Repository.ToListAsync<TDao, TEntity>(tracking).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> ToListAsync<TDao, TEntity>(WhereManyModel<TDao> model) where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity
        {
            return await Repository.ToListAsync<TDao, TEntity>(model).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> ToListAsync<TDao, TEntity>(WhereManyModel<TDao> model, ETracking tracking) where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity
        {
            return await Repository.ToListAsync<TDao, TEntity>(model, tracking).ConfigureAwait(false);
        }

        public async Task<bool> UpdateAsync<TEntity, TDao>(TEntity entity) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity
        {
            return await UpdateAsync<TEntity, TDao>(entity, 1).ConfigureAwait(false);
        }

        public async Task<bool> UpdateAsync<TEntity, TDao>(TEntity entity, int expectedSavedChangesCount) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity
        {
            using (var context = InstanciateContext())
            {
                Task<int> function() => UpdateAsync<TEntity, TDao>(context, entity);
                var result = await ExecuteAsync(function).ConfigureAwait(false);

                return result == expectedSavedChangesCount;
            }
        }

        #region Private methods

        private async Task<int> AddAsync<TEntity, TDao>(BaseDbContext context, TEntity entity) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity
        {
            var dao = new TDao();
            dao.Fill(entity);

            var dbSet = GetDbSet<TDao>(context);
            _ = dbSet.Add(dao);

            return await context.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task<int> DeleteAsync<TEntity, TDao>(BaseDbContext context, int ID) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity
        {
            var dbSet = GetDbSet<TDao>(context);
            var dao = await dbSet.FindAsync(ID).ConfigureAwait(false);
            _ = dbSet.Remove(dao);

            return await context.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task<int> UpdateAsync<TEntity, TDao>(BaseDbContext context, TEntity entity) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity
        {
            var dao = new TDao();
            dao.Fill(entity);

            var dbSet = GetDbSet<TDao>(context);
            var entry = await dbSet.FindAsync(entity.ID).ConfigureAwait(false);
            context.Entry(entry).CurrentValues.SetValues(dao);

            return await context.SaveChangesAsync().ConfigureAwait(false);
        }

        #endregion
    }
}
