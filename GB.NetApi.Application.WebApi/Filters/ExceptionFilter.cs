using GB.NetApi.Application.WebApi.Models.ObjectResults;
using GB.NetApi.Domain.Models.Enums;
using GB.NetApi.Domain.Models.Exceptions;
using GB.NetApi.Domain.Models.Interfaces.Libraries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace GB.NetApi.Application.WebApi.Filters
{
    /// <summary>
    /// Represents a filter which handles exception by formatting and returning them within the appropriated <see cref="ObjectResult"/> implementation
    /// </summary>
    public sealed class ExceptionFilter : IAsyncExceptionFilter, IExceptionFilter
    {
        #region Fields

        private const string BadRequestMessageLayout = "Bad request in action '{0}' of controller '{1}': {2}";

        #endregion

        public void OnException(ExceptionContext context)
        {
            context.Result = GetResultFromContextException(context);
            context.ExceptionHandled = true;
        }

        public async Task OnExceptionAsync(ExceptionContext context) => await Task.Run(() => OnException(context)).ConfigureAwait(false);

        #region Priate methods

        private static ObjectResult GetResultFromContextException(ExceptionContext context)
        {
            var logger = context.HttpContext.RequestServices.GetService(typeof(ILogger)) as ILogger;

            if (context.Exception is EntityValidationException)
            {
                var validationException = context.Exception as EntityValidationException;

                if (logger is not null)
                {
                    var actionName = context.HttpContext.Request.RouteValues["action"];
                    var controllerName = context.HttpContext.Request.RouteValues["controller"];
                    var message = string.Format(BadRequestMessageLayout, actionName, controllerName, string.Join(" | ", validationException.Errors));

                    logger.Log(ELogLevel.Warning, message);
                }

                return new BadRequestObjectResult(validationException.Errors);
            }

            if (logger is not null)
                logger.Log(context.Exception);

            return new InternalServerErrorObjectResult(GetMessageFromInnerException(context.Exception));
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
