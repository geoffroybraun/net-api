using GB.NetApi.Application.Services.Commands.Logs;
using GB.NetApi.Application.WebApi.Extensions;
using GB.NetApi.Application.WebApi.Formatters;
using GB.NetApi.Application.WebApi.Models.ObjectResults;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Text;
using System.Threading.Tasks;

namespace GB.NetApi.Application.WebApi.Middlewares
{
    /// <summary>
    /// Represents a middleware which handles exceptions raised within the pipeline
    /// </summary>
    public static class ExceptionMiddleware
    {
        #region Fields

        private static readonly JsonTextFormatter Formatter = new JsonTextFormatter();

        #endregion

        public static async Task RequestDelegate(HttpContext context)
        {
            var feature = context.Features.Get<IExceptionHandlerPathFeature>();

            if (context.RequestServices.TryGetService(out IMediator mediator))
                await mediator.RunAsync(new CreateExceptionLogCommand() { Exception = feature.Error }).ConfigureAwait(false);

            var result = new InternalServerErrorObjectResult(feature.Error.Message);
            var encodingName = context.Request.Headers[HeaderNames.AcceptEncoding].ToString();
            await WriteResultInResponseAsync(result, context.Response, context.Request.ContentType, SafelyGetEncoding(encodingName)).ConfigureAwait(false);
        }

        #region Private methods

        private static async Task WriteResultInResponseAsync(ObjectResult result, HttpResponse response, string contentType, Encoding encoding)
        {
            var serialiedResult = await Formatter.SerializeAsync(result, result.GetType()).ConfigureAwait(false);
            response.StatusCode = (int)result.StatusCode;
            response.ContentType = contentType;
            await response.Body.WriteAsync(encoding.GetBytes(serialiedResult)).ConfigureAwait(false);
        }

        private static Encoding SafelyGetEncoding(string encodingName)
        {
            try
            {
                return Encoding.GetEncoding(encodingName);
            }
            catch (Exception)
            {
                return Encoding.UTF8;
            }
        }

        #endregion
    }
}
