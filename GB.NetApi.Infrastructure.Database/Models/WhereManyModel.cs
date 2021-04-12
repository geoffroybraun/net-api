using GB.NetApi.Infrastructure.Database.DAOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GB.NetApi.Infrastructure.Database.Models
{
    /// <summary>
    /// Represents a model which provides functions to filter <see cref="TDao"/>
    /// </summary>
    /// <typeparam name="TDao">The DAO type to filter</typeparam>
    public sealed record WhereManyModel<TDao> where TDao : BaseDao
    {
        public IEnumerable<Expression<Func<TDao, bool>>> WhereMany { get; init; }
    }
}
