using GB.NetApi.Application.Services.Interfaces.Commands;
using GB.NetApi.Application.Services.Interfaces.Queries;
using GB.NetApi.Application.WebApi.Models.ObjectResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace GB.NetApi.Application.WebApi.Controllers
{
    /// <summary>
    /// Represents an abstract controller which provides useful methods to deriving classes
    /// </summary>
    [Produces("application/json")]
    public abstract class BaseController : ControllerBase
    {
        #region Fields

        private readonly IMediator Mediator;

        #endregion

        protected BaseController(IMediator mediator) => Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        /// <summary>
        /// Creates a <see cref="InternalServerErrorResult"/> object that produces a <see cref="StatusCodes.Status500InternalServerError"/> response
        /// </summary>
        /// <returns>The created <see cref="InternalServerErrorResult"/> response</returns>
        protected InternalServerErrorResult InternalServerError() => new InternalServerErrorResult();

        /// <summary>
        /// Execute the provided <see cref="IQuery{TResult}"/> to retrieve its result
        /// </summary>
        /// <typeparam name="TResult">The query result type</typeparam>
        /// <param name="query">The <see cref="IQuery{TResult}"/> to execute</param>
        /// <returns>The query result</returns>
        protected async Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> query) => await Mediator.Send(query).ConfigureAwait(false);

        /// <summary>
        /// Run the provided <see cref="ICommand{TResult}"/> to retrieve its result
        /// </summary>
        /// <typeparam name="TResult">The command result type</typeparam>
        /// <param name="command">The <see cref="ICommand{TResult}"/> to run</param>
        /// <returns>The command result</returns>
        protected async Task<TResult> RunAsync<TResult>(ICommand<TResult> command) => await Mediator.Send(command).ConfigureAwait(false);
    }
}
