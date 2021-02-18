using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Services.Extensions;
using GB.NetApi.Infrastructure.Database.Contexts;
using GB.NetApi.Infrastructure.Database.DAOs;
using GB.NetApi.Infrastructure.Database.Enums;
using GB.NetApi.Infrastructure.Database.Extensions;
using GB.NetApi.Infrastructure.Database.Interfaces;
using GB.NetApi.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GB.NetApi.Infrastructure.Database.Repositories.Commons
{
    public sealed class CommonReadableRepository : ICommonReadableRepository
    {
        #region Fields

        private readonly ICommonRepository Repository;

        #endregion

        public CommonReadableRepository(ICommonRepository repository) => Repository = repository ?? throw new ArgumentNullException(nameof(repository));

        public async Task<bool> AnyAsync<TDao, TEntity>(AnyModel<TDao> model) where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity
        {
             return await AnyAsync<TDao, TEntity>(model, ETracking.Disabled).ConfigureAwait(false);
        }

        public async Task<bool> AnyAsync<TDao, TEntity>(AnyModel<TDao> model, ETracking tracking) where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity
        {
            using (var context = InstanciateContext())
            {
                Task<bool> function() => GetQuery<TDao>(context, tracking).AnyAsync(model.Any);

                return await ExecuteAsync(function).ConfigureAwait(false);
            }
        }

        public async Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> taskFunction) => await Repository.ExecuteAsync(taskFunction).ConfigureAwait(false);

        public DbSet<TDao> GetDbSet<TDao>(BaseDbContext context) where TDao : class => Repository.GetDbSet<TDao>(context);

        public IQueryable<TDao> GetQuery<TDao>(BaseDbContext context) where TDao : class => Repository.GetQuery<TDao>(context);

        public IQueryable<TDao> GetQuery<TDao>(BaseDbContext context, ETracking tracking) where TDao : class => Repository.GetQuery<TDao>(context, tracking);

        public BaseDbContext InstanciateContext() => Repository.InstanciateContext();

        public async Task<TEntity> SingleAsync<TDao, TEntity>(SingleModel<TDao> model) where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity
        {
            return await SingleAsync<TDao, TEntity>(model, ETracking.Disabled).ConfigureAwait(false);
        }

        public async Task<TEntity> SingleAsync<TDao, TEntity>(SingleModel<TDao> model, ETracking tracking) where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity
        {
            using (var context = InstanciateContext())
            {
                Task<TDao> function() => GetQuery<TDao>(context, tracking).SingleOrDefaultAsync(model.SingleOrDefault);
                var result = await ExecuteAsync(function).ConfigureAwait(false);

                return Transform<TDao, TEntity>(result);
            }
        }

        public async Task<IEnumerable<TEntity>> ToListAsync<TDao, TEntity>() where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity
        {
            return await ToListAsync<TDao, TEntity>(ETracking.Disabled).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> ToListAsync<TDao, TEntity>(ETracking tracking) where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity
        {
            using (var context = InstanciateContext())
            {
                Task<List<TDao>> function() => GetQuery<TDao>(context, tracking).ToListAsync();
                var result = await ExecuteAsync(function).ConfigureAwait(false);

                return Transform<TDao, TEntity>(result);
            }
        }

        public async Task<IEnumerable<TEntity>> ToListAsync<TDao, TEntity>(WhereManyModel<TDao> model) where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity
        {
            return await ToListAsync<TDao, TEntity>(model, ETracking.Disabled).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> ToListAsync<TDao, TEntity>(WhereManyModel<TDao> model, ETracking tracking) where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity
        {
            using (var context = InstanciateContext())
            {
                Task<List<TDao>> function() => GetQuery<TDao>(context, tracking)
                    .WhereMany(model.WhereMany)
                    .ToListAsync();
                var result = await ExecuteAsync(function).ConfigureAwait(false);

                return Transform<TDao, TEntity>(result);
            }
        }

        #region Private methods

        private static IEnumerable<TEntity> Transform<TDao, TEntity>(IEnumerable<TDao> daos) where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity
        {
            return daos.IsNotNullNorEmpty() ? daos.Select(d => Transform<TDao, TEntity>(d)) : default;
        }

        private static TEntity Transform<TDao, TEntity>(TDao dao) where TDao : BaseReadableDao<TEntity> where TEntity : BaseStorableEntity
        {
            return dao is not null ? dao.Transform() : default;
        }

        #endregion
    }
}
