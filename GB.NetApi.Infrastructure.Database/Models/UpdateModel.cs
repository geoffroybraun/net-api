using GB.NetApi.Infrastructure.Database.Contexts;
using GB.NetApi.Infrastructure.Database.DAOs;
using System;

namespace GB.NetApi.Infrastructure.Database.Models
{
    /// <summary>
    /// Represents a model which provides a function setting a <see cref="TDao"/> properties before updating it
    /// </summary>
    /// <typeparam name="TDao">The DAO type to update</typeparam>
    public sealed record UpdateModel<TDao> where TDao : BaseDao
    {
        public int ID { get; init; }

        public bool HasPreUpdate => ExecutePreUpdate is not null;

        public Action<BaseDbContext> ExecutePreUpdate { get; init; }

        public Action<TDao> UpdateDaoProperties { get; init; }

        public bool HasPostUpdate => ExecutePostUpdate is not null;

        public Action<BaseDbContext, TDao> ExecutePostUpdate { get; init; }
    }
}
