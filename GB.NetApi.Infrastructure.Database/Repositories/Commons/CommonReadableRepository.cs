using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Interfaces.Entities;
using GB.NetApi.Domain.Services.Extensions;
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

        public async Task<bool> AnyAsync<TDao, TEntity>(AnyModel<TDao> model) where TDao : BaseDao, ITransformable<TEntity> where TEntity : BaseStorableEntity
        {
             return await AnyAsync<TDao, TEntity>(model, ETracking.Disabled).ConfigureAwait(false);
        }

        public async Task<bool> AnyAsync<TDao, TEntity>(AnyModel<TDao> model, ETracking tracking) where TDao : BaseDao, ITransformable<TEntity> where TEntity : BaseStorableEntity
        {
            using (var context = Repository.InstanciateContext())
            {
                Task<bool> function() => Repository.GetQuery<TDao>(context, tracking).AnyAsync(model.Any);

                return await Repository.ExecuteAsync(function).ConfigureAwait(false);
            }
        }

        public async Task<TEntity> SingleAsync<TDao, TEntity>(SingleModel<TDao> model) where TDao : BaseDao, ITransformable<TEntity> where TEntity : BaseStorableEntity
        {
            return await SingleAsync<TDao, TEntity>(model, ETracking.Disabled).ConfigureAwait(false);
        }

        public async Task<TEntity> SingleAsync<TDao, TEntity>(SingleModel<TDao> model, ETracking tracking) where TDao : BaseDao, ITransformable<TEntity> where TEntity : BaseStorableEntity
        {
            using (var context = Repository.InstanciateContext())
            {
                Task<TDao> function() => Repository.GetQuery<TDao>(context, tracking).SingleOrDefaultAsync(model.SingleOrDefault);
                var result = await Repository.ExecuteAsync(function).ConfigureAwait(false);

                return Transform<TDao, TEntity>(result);
            }
        }

        public async Task<IEnumerable<TEntity>> ToListAsync<TDao, TEntity>() where TDao : BaseDao, ITransformable<TEntity> where TEntity : BaseStorableEntity
        {
            return await ToListAsync<TDao, TEntity>(ETracking.Disabled).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> ToListAsync<TDao, TEntity>(ETracking tracking) where TDao : BaseDao, ITransformable<TEntity> where TEntity : BaseStorableEntity
        {
            using (var context = Repository.InstanciateContext())
            {
                Task<List<TDao>> function() => Repository.GetQuery<TDao>(context, tracking).ToListAsync();
                var result = await Repository.ExecuteAsync(function).ConfigureAwait(false);

                return Transform<TDao, TEntity>(result);
            }
        }

        public async Task<IEnumerable<TEntity>> ToListAsync<TDao, TEntity>(WhereManyModel<TDao> model) where TDao : BaseDao, ITransformable<TEntity> where TEntity : BaseStorableEntity
        {
            return await ToListAsync<TDao, TEntity>(model, ETracking.Disabled).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> ToListAsync<TDao, TEntity>(WhereManyModel<TDao> model, ETracking tracking) where TDao : BaseDao, ITransformable<TEntity> where TEntity : BaseStorableEntity
        {
            using (var context = Repository.InstanciateContext())
            {
                Task<List<TDao>> function() => Repository.GetQuery<TDao>(context, tracking)
                    .WhereMany(model.WhereMany)
                    .ToListAsync();
                var result = await Repository.ExecuteAsync(function).ConfigureAwait(false);

                return Transform<TDao, TEntity>(result);
            }
        }

        #region Private methods

        private static IEnumerable<TEntity> Transform<TDao, TEntity>(IEnumerable<TDao> daos) where TDao : BaseDao, ITransformable<TEntity> where TEntity : BaseStorableEntity
        {
            return daos.IsNotNullNorEmpty() ? daos.Select(d => Transform<TDao, TEntity>(d)) : default;
        }

        private static TEntity Transform<TDao, TEntity>(TDao dao) where TDao : BaseDao, ITransformable<TEntity> where TEntity : BaseStorableEntity
        {
            return dao is not null ? dao.Transform() : default;
        }

        #endregion
    }
}
