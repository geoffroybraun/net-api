using GB.NetApi.Infrastructure.Database.Contexts;
using GB.NetApi.Infrastructure.Database.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GB.NetApi.Infrastructure.Database.Interfaces
{
    /// <summary>
    /// Represents a common repository which provides access to an <see cref="IQueryable{T}"/> implementation or a <see cref="DbSet{T}"/> one
    /// </summary>
    public interface ICommonRepository
    {
        /// <summary>
        /// Execute the provides task function
        /// </summary>
        /// <typeparam name="TResult">The task function result type</typeparam>
        /// <param name="taskFunction">The task function to execute</param>
        /// <returns>The task function result</returns>
        Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> taskFunction);

        /// <summary>
        /// Instanciate a <see cref="BaseDbContext"/> implementation
        /// </summary>
        /// <returns>A new <see cref="BaseDbContext"/> implementation instance</returns>
        BaseDbContext InstanciateContext();

        /// <summary>
        /// Returns the related <see cref="IQueryable{T}"/> implementation from the provided <see cref="BaseDbContext"/> one
        /// </summary>
        /// <typeparam name="TDao">The DAO type to query</typeparam>
        /// <param name="context">The <see cref="BaseDbContext"/> implementation where to look for the <see cref="IQueryable{T}"/> implementation</param>
        /// <returns>The found <see cref="IQueryable{T}"/> implementation</returns>
        IQueryable<TDao> GetQuery<TDao>(BaseDbContext context) where TDao : class;

        /// <summary>
        /// Returns the related <see cref="IQueryable{T}"/> implementation from the provided <see cref="BaseDbContext"/> one
        /// </summary>
        /// <typeparam name="TDao">The DAO type to query</typeparam>
        /// <param name="context">The <see cref="BaseDbContext"/> implementation where to look for the <see cref="IQueryable{T}"/> implementation</param>
        /// <param name="tracking">The tracking mode to use when querying</param>
        /// <returns>The found <see cref="IQueryable{T}"/> implementation</returns>
        IQueryable<TDao> GetQuery<TDao>(BaseDbContext context, ETracking tracking) where TDao : class;

        /// <summary>
        /// Returns the related <see cref="DbSet{T}"/> implementation from the provided <see cref="BaseDbContext"/> one
        /// </summary>
        /// <typeparam name="TDao">The DAO type to query</typeparam>
        /// <param name="context">The <see cref="BaseDbContext"/> implementation where to look for the <see cref="DbSet{T}"/> implementation</param>
        /// <returns>The found <see cref="DbSet{T}"/> implementation</returns>
        DbSet<TDao> GetDbSet<TDao>(BaseDbContext context) where TDao : class;
    }
}
