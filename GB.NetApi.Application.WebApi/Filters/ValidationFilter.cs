using GB.NetApi.Application.Services.Commands.Logs;
using GB.NetApi.Application.WebApi.Extensions;
using MediatR;
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
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.SelectMany(e => e.Value.Errors.Select(e => e.ErrorMessage));
                context.Result = new BadRequestObjectResult(errors);

                if (context.HttpContext.RequestServices.TryGetService(out IMediator mediator))
                {
                    var command = new CreateBadRequestLogCommand()
                    {
                        ActionName = (context.Controller as ControllerBase).ControllerContext.ActionDescriptor.ActionName,
                        ControllerName = context.Controller.GetType().Name,
                        Errors = errors
                    };
                    _ = await mediator.Send(command).ConfigureAwait(false);
                }

                return;
            }

            await next().ConfigureAwait(false);
        }
    }
}
