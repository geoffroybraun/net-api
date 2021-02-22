using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace GB.NetApi.Application.WebApi.Authorizations
{
    /// <summary>
    /// Represents ana authorization handler which uses permissions
    /// </summary>
    public sealed class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
        {
            var claims = context.User?.Claims.Where(c => c.Type == "Permission");

            if (claims is not null && claims.Any(c => c.Value == requirement.PermissionName))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
