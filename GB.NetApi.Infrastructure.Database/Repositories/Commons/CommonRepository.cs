using GB.NetApi.Domain.Models.Interfaces.Libraries;
using GB.NetApi.Infrastructure.Database.Contexts;
using GB.NetApi.Infrastructure.Database.Enums;
using GB.NetApi.Infrastructure.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GB.NetApi.Infrastructure.Database.Repositories.Commons
{
    public sealed class CommonRepository : ICommonRepository
    {
        #region Fields

        private readonly Func<BaseDbContext> ContextFunction;
        private readonly ITaskHandler TaskHandler;

        #endregion

        public CommonRepository(Func<BaseDbContext> contextFunction, ITaskHandler taskHandler)
        {
            ContextFunction = contextFunction ?? throw new ArgumentNullException(nameof(contextFunction));
            TaskHandler = taskHandler ?? throw new ArgumentNullException(nameof(taskHandler));
        }

        public async Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> taskFunction) => await TaskHandler.HandleAsync(taskFunction).ConfigureAwait(false);

        public BaseDbContext InstanciateContext() => ContextFunction();

        public DbSet<TDao> GetDbSet<TDao>(BaseDbContext context) where TDao : class
        {
            var properties = typeof(BaseDbContext).GetProperties();
            var dbSetProperty = properties.SingleOrDefault(p => p.PropertyType == typeof(DbSet<TDao>));

            if (dbSetProperty is null)
                throw new ArgumentException($"No property of '{typeof(DbSet<TDao>).Name}' has been found in context '{context.GetType().Name}'...");

            return dbSetProperty.GetValue(context) as DbSet<TDao>;
        }

        public IQueryable<TDao> GetQuery<TDao>(BaseDbContext context) where TDao : class
        {
            return GetQuery<TDao>(context, ETracking.Disabled);
        }

        public IQueryable<TDao> GetQuery<TDao>(BaseDbContext context, ETracking tracking) where TDao : class
        {
            var dbSet = GetDbSet<TDao>(context);

            return tracking == ETracking.Disabled ? dbSet.AsNoTracking() : dbSet;
        }
    }
}
