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
        /// Returns the related <see cref="DbSet{TDao}"/> property from the provided <see cref="BaseDbContext"/> implementation based on the required <see cref="ETracking"/> mode
        /// </summary>
        /// <param name="context">The <see cref="BaseDbContext"/> implementation where to find a <see cref="DbSet{TDao}"/> property</param>
        /// <returns>The found <see cref="DbSet{TDao}"/> property</returns>
        IQueryable<TDao> GetQuery<TDao>(BaseDbContext context) where TDao : class;

        /// <summary>
        /// Returns the related <see cref="DbSet{TDao}"/> property from the provided <see cref="BaseDbContext"/> implementation based on the required <see cref="ETracking"/> mode
        /// </summary>
        /// <param name="context">The <see cref="BaseDbContext"/> implementation where to find a <see cref="DbSet{TDao}"/> property</param>
        /// <param name="tracking">The tracking mode to use when querying</param>
        /// <returns>The found <see cref="DbSet{TDao}"/> property</returns>
        IQueryable<TDao> GetQuery<TDao>(BaseDbContext context, ETracking tracking) where TDao : class;

        /// <summary>
        /// Returns the related <see cref="DbSet{TDao}"/> property from the provided <see cref="BaseDbContext"/> implementation
        /// </summary>
        /// <param name="context">The <see cref="BaseDbContext"/> implementation where to find a <see cref="DbSet{TDao}"/> property</param>
        /// <returns>The found <see cref="DbSet{TDao}"/> property</returns>
        DbSet<TDao> GetDbSet<TDao>(BaseDbContext context) where TDao : class;
    }
}
