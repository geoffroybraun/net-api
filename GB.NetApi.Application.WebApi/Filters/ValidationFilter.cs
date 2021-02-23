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

                return;
            }

            await next().ConfigureAwait(false);
        }
    }
}
