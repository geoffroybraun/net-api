using GB.NetApi.Domain.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GB.NetApi.Infrastructure.Database.Extensions
{
    /// <summary>
    /// Extends an <see cref="IQueryable{T}"/> implementation
    /// </summary>
    public static class IQueryableExtension
    {
        /// <summary>
        /// Add multiple 'where' clauses to the extended <see cref="IQueryable{T}"/> implementation
        /// </summary>
        /// <typeparam name="TDao">The DAO type to filter</typeparam>
        /// <param name="source">The extended <see cref="IQueryable{T}"/> implementation to filter</param>
        /// <param name="functions">The filtering functions to apply as 'where' clauses</param>
        /// <returns>The filtered <see cref="IQueryable{T}"/> implementation</returns>
        public static IQueryable<TDao> WhereMany<TDao>(this IQueryable<TDao> source, IEnumerable<Expression<Func<TDao, bool>>> functions)
        {
            if (functions.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(functions));

            foreach (var function in functions)
                source = source.Where(function);

            return source;
        }
    }
}
