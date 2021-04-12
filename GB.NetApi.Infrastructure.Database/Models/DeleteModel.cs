using GB.NetApi.Infrastructure.Database.Contexts;
using GB.NetApi.Infrastructure.Database.DAOs;
using System;

namespace GB.NetApi.Infrastructure.Database.Models
{
    public sealed class DeleteModel<TDao> where TDao : BaseDao
    {
        public int ID { get; init; }

        public bool HasPreDelete => ExecutePreDelete is not null;

        public Action<BaseDbContext> ExecutePreDelete { get; init; }

        public bool HasPostDelete => ExecutePostDelete is not null;

        public Action<BaseDbContext, TDao> ExecutePostDelete { get; init; }
    }
}
