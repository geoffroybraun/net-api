using System;
using System.Threading.Tasks;

namespace GB.NetApi.Domain.Models.Interfaces.Libraries
{
    /// <summary>
    /// Represents a task handler which applies several policies like retry, fallback, etc.
    /// </summary>
    public interface ITaskHandler
    {
        /// <summary>
        /// Execute the provided task function to handle it using several policies
        /// </summary>
        /// <typeparam name="TResult">The result type</typeparam>
        /// <param name="taskFunction">The task function to handle using several policies</param>
        /// <returns>The handled task function</returns>
        Task<TResult> HandleAsync<TResult>(Func<Task<TResult>> taskFunction);
    }
}
