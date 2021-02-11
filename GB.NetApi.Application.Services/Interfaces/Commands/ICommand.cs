using MediatR;

namespace GB.NetApi.Application.Services.Interfaces.Commands
{
    /// <summary>
    /// Represents a command which returns a <see cref="TResult"/> when handled
    /// </summary>
    /// <typeparam name="TResult">The result type when handled</typeparam>
    public interface ICommand<out TResult> : IRequest<TResult> { }
}
