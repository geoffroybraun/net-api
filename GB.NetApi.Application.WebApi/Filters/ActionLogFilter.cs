using GB.NetApi.Application.Services.Commands.Logs;
using GB.NetApi.Application.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GB.NetApi.Application.WebApi.Filters
{
    /// <summary>
    /// Represents a filter which logs a message for every called controller action
    /// </summary>
    public sealed class ActionLogFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var watch = new Stopwatch();
            watch.Start();

            await next();

            watch.Stop();

            if (!context.HttpContext.RequestServices.TryGetService(out IMediator mediator))
                return;

            var command = new CreateActionLogCommand()
            {
                ActionName = (context.Controller as ControllerBase).ControllerContext.ActionDescriptor.ActionName,
                ControllerName = context.Controller.GetType().Name,
                ExecutionTime = watch.Elapsed.TotalMilliseconds
            };
            _ = await mediator.RunAsync(command).ConfigureAwait(false);
        }
    }
}
