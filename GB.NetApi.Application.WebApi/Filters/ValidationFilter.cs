using GB.NetApi.Application.WebApi.Extensions;
using GB.NetApi.Domain.Models.Enums;
using GB.NetApi.Domain.Models.Interfaces.Libraries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace GB.NetApi.Application.WebApi.Filters
{
    /// <summary>
    /// Represents a filter which handles a invalid model to returns all error messages within a <see cref="BadRequestObjectResult"/>
    /// </summary>
    public sealed class ValidationFilter : IAsyncActionFilter
    {
        #region Fields

        private const string MessageLayout = "Bad request in action '{0}' of controller '{1}': {2}";

        #endregion

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.SelectMany(e => e.Value.Errors.Select(e => e.ErrorMessage));
                context.Result = new BadRequestObjectResult(errors);

                if (context.HttpContext.RequestServices.TryGetService(out ILogger logger))
                {
                    var controllerName = context.Controller.GetType().FullName;
                    var actionName = (context.Controller as ControllerBase).ControllerContext.ActionDescriptor.ActionName;
                    var message = string.Format(MessageLayout, actionName, controllerName, string.Join(" | ", errors));

                    logger.Log(ELogLevel.Warning, message);
                }

                return;
            }

            await next().ConfigureAwait(false);
        }
    }
}
