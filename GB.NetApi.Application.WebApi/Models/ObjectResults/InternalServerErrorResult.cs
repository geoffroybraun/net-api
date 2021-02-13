using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace GB.NetApi.Application.WebApi.Models.ObjectResults
{
    /// <summary>
    /// Represents an <see cref="StatusCodeResult"/> implementation which returns a 500 http status code in case of internal server error
    /// </summary>
    [DefaultStatusCode(DefaultStatusCode)]
    public sealed class InternalServerErrorResult : StatusCodeResult
    {
        #region Fields

        private const int DefaultStatusCode = StatusCodes.Status500InternalServerError;

        #endregion

        public InternalServerErrorResult() : base(DefaultStatusCode) { }
    }
}
