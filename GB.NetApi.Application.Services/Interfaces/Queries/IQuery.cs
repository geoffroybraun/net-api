using MediatR;

namespace GB.NetApi.Application.Services.Interfaces.Queries
{
    /// <summary>
    /// Represents a query which returns a <see cref="TResult"/> when handled
    /// </summary>
    /// <typeparam name="TResult">The result type when handled</typeparam>
    public interface IQuery<out TResult> : IRequest<TResult> { }
}
