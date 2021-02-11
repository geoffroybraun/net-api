using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GB.NetApi.Application.WebApi.Controllers
{
    /// <summary>
    /// Represents an abstract controller which provides useful methods to deriving classes
    /// </summary>
    public abstract class BaseController : ControllerBase
    {
        #region Fields

        private readonly IMediator Mediator;

        #endregion

        protected BaseController(IMediator mediator) => Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        /// <summary>
        /// Send the provided <see cref="IRequest{TResult}"/> to return the related result
        /// </summary>
        /// <typeparam name="TResult">The result type</typeparam>
        /// <param name="request">The <see cref="IRequest{TResult}"/> to send</param>
        /// <returns>The <see cref="IRequest{TResult}"/> result</returns>
        protected async Task<TResult> SendAsync<TResult>(IRequest<TResult> request) => await Mediator.Send(request).ConfigureAwait(false);
    }
}
