using GB.NetApi.Application.WebApi.Extensions;
using GB.NetApi.Domain.Models.Enums;
using GB.NetApi.Domain.Models.Interfaces.Libraries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace GB.NetApi.Application.WebApi.Filters
{
    /// <summary>
    /// Represents a filter which logs a message for every called controller action
    /// </summary>
    public sealed class ActionLogFilter : IActionFilter
    {
        #region Fields

        private const string ActionLogFilterWatch = "ActionLogFilterWatch";
        private const string MessageLayout = "Controller: '{0}' | Action: '{1}' [{2}ms]";

        #endregion

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var watch = context.HttpContext.Items[ActionLogFilterWatch] as Stopwatch;
            watch.Stop();

            if (!context.HttpContext.RequestServices.TryGetService(out ILogger logger))
                return;

            var elapsed = watch.Elapsed.TotalMilliseconds;
            var controllerName = context.Controller.GetType().FullName;
            var methodName = (context.Controller as ControllerBase).ControllerContext.ActionDescriptor.ActionName;
            var message = string.Format(MessageLayout, controllerName, methodName, elapsed);

            logger.Log(ELogLevel.Information, message);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var watch = new Stopwatch();
            watch.Start();
            context.HttpContext.Items.Add(ActionLogFilterWatch, watch);
        }
    }
}
