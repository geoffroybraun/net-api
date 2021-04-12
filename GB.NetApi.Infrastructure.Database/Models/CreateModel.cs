using GB.NetApi.Infrastructure.Database.Contexts;
using GB.NetApi.Infrastructure.Database.DAOs;
using System;

namespace GB.NetApi.Infrastructure.Database.Models
{
    /// <summary>
    /// Represents a model which provides a function setting a <see cref="TDao"/> properties before creating it
    /// </summary>
    /// <typeparam name="TDao">The DAO type to create</typeparam>
    public sealed record CreateModel<TDao> where TDao : BaseDao
    {
        public bool HasPreCreate => ExecutePreCreate is not null;

        public Action<BaseDbContext> ExecutePreCreate { get; init; }

        public Action<TDao> SetDaoProperties { get; init; }

        public bool HasPostCreate => ExecutePostCreate is not null;

        public Action<BaseDbContext, TDao> ExecutePostCreate { get; init; }
    }
}
