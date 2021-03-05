using GB.NetApi.Application.WebApi.Models.ObjectResults;
using GB.NetApi.Domain.Models.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace GB.NetApi.Application.WebApi.Controllers
{
    /// <summary>
    /// Represents an abstract controller which provides useful methods to deriving classes
    /// </summary>
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public abstract class BaseController : ControllerBase
    {
        #region Properties

        protected readonly IMediator Mediator;
        protected readonly ITranslator Translator;

        #endregion

        /// <summary>
        /// Instanciates a deriving class
        /// </summary>
        /// <param name="mediator">The <see cref="IMediator"/> implementation to use</param>
        /// <param name="translator">The <see cref="ITranslator"/> implementation to use</param>
        protected BaseController(IMediator mediator, ITranslator translator)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            Translator = translator ?? throw new ArgumentNullException(nameof(translator));
        }

        /// <summary>
        /// Creates a <see cref="InternalServerErrorResult"/> object that produces a <see cref="StatusCodes.Status500InternalServerError"/> response
        /// </summary>
        /// <returns>The created <see cref="InternalServerErrorResult"/> response</returns>
        protected InternalServerErrorResult InternalServerError() => new InternalServerErrorResult();
    }
}
