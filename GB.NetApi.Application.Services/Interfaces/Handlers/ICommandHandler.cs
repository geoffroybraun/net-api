using GB.NetApi.Application.Services.Interfaces.Commands;
using MediatR;

namespace GB.NetApi.Application.Services.Interfaces.Handlers
{
    /// <summary>
    /// Represents a command handler which handles a <see cref="TCommand"/> to return a <see cref="TResult"/>
    /// </summary>
    /// <typeparam name="TCommand">The command type to handle</typeparam>
    /// <typeparam name="TResult">The result type to return</typeparam>
    public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult> { }
}
