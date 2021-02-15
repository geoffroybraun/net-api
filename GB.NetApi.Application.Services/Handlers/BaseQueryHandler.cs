using GB.NetApi.Application.Services.Interfaces.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GB.NetApi.Application.Services.Handlers
{
    /// <summary>
    /// Represents an abstract handler which executes a <see cref="TQuery"/>
    /// </summary>
    /// <typeparam name="TQuery">The query type to execute</typeparam>
    /// <typeparam name="TResult">The query result type to return</typeparam>
    public abstract class BaseQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        public async Task<TResult> Handle(TQuery request, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Delegate the method implementation to the deriving class
        /// </summary>
        /// <param name="query">The <see cref="TQuery"/> to execute</param>
        /// <returns>The <see cref="TQuery"/> result</returns>
        public abstract Task<TResult> ExecuteAsync(TQuery query);
    }
}
