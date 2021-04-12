using GB.NetApi.Infrastructure.Database.DAOs;
using System;
using System.Linq.Expressions;

namespace GB.NetApi.Infrastructure.Database.Models
{
    /// <summary>
    /// Represents a model which provides a function to retrieve a single <see cref="TDao"/>
    /// </summary>
    /// <typeparam name="TDao">The DAO type to query</typeparam>
    public sealed record SingleModel<TDao> where TDao : BaseDao
    {
        public Expression<Func<TDao, bool>> SingleOrDefault { get; init; }
    }
}
