using GB.NetApi.Application.Services.Interfaces.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GB.NetApi.Application.Services.Handlers
{
    /// <summary>
    /// Represents an abstract handler which run a <see cref="TCommand"/>
    /// </summary>
    /// <typeparam name="TCommand">The command type to run</typeparam>
    /// <typeparam name="TResult">The command result type to return</typeparam>
    public abstract class BaseCommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
        public async Task<TResult> Handle(TCommand request, CancellationToken cancellationToken)
        {
            return await RunAsync(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Delegate the method implementation to the deriving class
        /// </summary>
        /// <param name="command">The <see cref="TCommand"/> to run</param>
        /// <returns>The <see cref="TCommand"/> result</returns>
        public abstract Task<TResult> RunAsync(TCommand command);
    }
}
