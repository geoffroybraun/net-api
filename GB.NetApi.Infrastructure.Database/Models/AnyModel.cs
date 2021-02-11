using System;
using System.Linq.Expressions;

namespace GB.NetApi.Infrastructure.Database.Models
{
    /// <summary>
    /// Represents a model which provides a function to look for <see cref="TDao"/>
    /// </summary>
    /// <typeparam name="TDao">The DAO type to look for</typeparam>
    public sealed record AnyModel<TDao>
    {
        public Expression<Func<TDao, bool>> Any { get; init; }
    }
}
