using GB.NetApi.Application.Services.Interfaces.Queries;
using MediatR;

namespace GB.NetApi.Application.Services.Interfaces.Handlers
{
    /// <summary>
    /// Represents a query handler which handles a <see cref="TQuery"/> to return a <see cref="TResult"/>
    /// </summary>
    /// <typeparam name="TQuery">The query type to handle</typeparam>
    /// <typeparam name="TResult">The result type to return</typeparam>
    public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult> { }
}
