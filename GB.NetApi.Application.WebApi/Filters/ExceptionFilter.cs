using GB.NetApi.Application.Services.Commands.Logs;
using GB.NetApi.Application.WebApi.Extensions;
using GB.NetApi.Application.WebApi.Models.ObjectResults;
using GB.NetApi.Domain.Models.Exceptions;
using GB.NetApi.Domain.Models.Interfaces.Libraries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GB.NetApi.Application.WebApi.Filters
{
    /// <summary>
    /// Represents a filter which handles exception by formatting and returning them within the appropriated <see cref="ObjectResult"/> implementation
    /// </summary>
    public sealed class ExceptionFilter : IAsyncExceptionFilter, IExceptionFilter
    {
        public void OnException(ExceptionContext context) => Task.Run(() => OnExceptionAsync(context));

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            context.Result = await GetResultFromContextException(context).ConfigureAwait(false);
            context.ExceptionHandled = true;
        }

        #region Priate methods

        private static async Task<ObjectResult> GetResultFromContextException(ExceptionContext context)
        {
            var logger = context.HttpContext.RequestServices.GetService(typeof(ILogger)) as ILogger;

            if (context.Exception is EntityValidationException)
            {
                var validationException = context.Exception as EntityValidationException;
                await LogBadRequestAsync(context, validationException.Errors).ConfigureAwait(false);                

                return new BadRequestObjectResult(validationException.Errors);
            }

            if (logger is not null)
                logger.Log(context.Exception);

            return new InternalServerErrorObjectResult(GetMessageFromInnerException(context.Exception));
        }

        private static async Task LogBadRequestAsync(ExceptionContext context, IEnumerable<string> errors)
        {
            if (context.HttpContext.RequestServices.TryGetService(out IMediator mediator))
            {
                var command = new CreateBadRequestLogCommand()
                {
                    ActionName = context.HttpContext.Request.RouteValues["action"].ToString(),
                    ControllerName = context.HttpContext.Request.RouteValues["controller"].ToString(),
                    Errors = errors
                };
                _ = await mediator.RunAsync(command).ConfigureAwait(false);
            }
        }

        private static string GetMessageFromInnerException(Exception exception)
        {
            if (exception.InnerException is not null)
                return GetMessageFromInnerException(exception.InnerException);

            return exception.Message;
        }

        #endregion
    }
}
