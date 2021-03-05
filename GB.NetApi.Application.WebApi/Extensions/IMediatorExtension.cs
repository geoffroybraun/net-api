using GB.NetApi.Application.Services.Interfaces.Commands;
using GB.NetApi.Application.Services.Interfaces.Queries;
using MediatR;
using System.Threading.Tasks;

namespace GB.NetApi.Application.WebApi.Extensions
{
    /// <summary>
    /// Extends an <see cref="IMediator"/> implementation
    /// </summary>
    public static class IMediatorExtension
    {
        /// <summary>
        /// Execute the provided <see cref="IQuery{TResult}"/> implementation
        /// </summary>
        /// <typeparam name="TResult">The query result type</typeparam>
        /// <param name="mediator">The extended <see cref="IMediator"/> implementation to use when executing the query</param>
        /// <param name="query">The <see cref="IQuery{TResult}"/> implementation to execute</param>
        /// <returns>The query result</returns>
        public static async Task<TResult> ExecuteAsync<TResult>(this IMediator mediator, IQuery<TResult> query)
        {
            return await mediator.Send(query).ConfigureAwait(false);
        }

        /// <summary>
        /// Run the provided <see cref="ICommand{TResult}"/> implementation
        /// </summary>
        /// <typeparam name="TResult">The command result type</typeparam>
        /// <param name="mediator">The extended <see cref="IMediator"/> implementation to use when running the command</param>
        /// <param name="command">The <see cref="ICommand{TResult}"/> implementation to run</param>
        /// <returns>The command result</returns>
        public static async Task<TResult> RunAsync<TResult>(this IMediator mediator, ICommand<TResult> command)
        {
            return await mediator.Send(command).ConfigureAwait(false);
        }
    }
}
