using GB.NetApi.Application.WebApi.Models.ObjectResults;
using GB.NetApi.Domain.Models.Exceptions;
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
        public void OnException(ExceptionContext context)
        {
            context.Result = GetResultFromContextException(context.Exception);
            context.ExceptionHandled = true;
        }

        public async Task OnExceptionAsync(ExceptionContext context) => await Task.Run(() => OnException(context)).ConfigureAwait(false);

        #region Priate methods

        private static ObjectResult GetResultFromContextException(Exception exception)
        {
            if (exception is EntityValidationException)
            {
                var validationException = exception as EntityValidationException;

                return new BadRequestObjectResult(validationException.Errors);
            }

            return new InternalServerErrorObjectResult(GetMessageFromInnerException(exception));
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
