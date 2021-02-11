using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace GB.NetApi.Application.WebApi.Models.ObjectResults
{
    /// <summary>
    /// Represents an <see cref="ObjectResult"/> implementation which returns a 500 http status code in case of internal server error
    /// </summary>
    [DefaultStatusCode(DefaultStatusCode)]
    public sealed class InternalServerErrorObjectResult : ObjectResult
    {
        #region Fields

        private const int DefaultStatusCode = StatusCodes.Status500InternalServerError;

        #endregion

        public InternalServerErrorObjectResult([ActionResultObjectValue] object value) : base(value) => StatusCode = DefaultStatusCode;

        public InternalServerErrorObjectResult([ActionResultObjectValue] ModelStateDictionary modelState) : base(new SerializableError(modelState))
        {
            if (modelState is null)
                throw new ArgumentNullException(nameof(modelState));

            StatusCode = DefaultStatusCode;
        }
    }
}
