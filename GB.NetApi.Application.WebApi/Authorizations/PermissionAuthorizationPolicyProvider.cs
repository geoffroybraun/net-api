using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace GB.NetApi.Application.WebApi.Authorizations
{
    /// <summary>
    /// Represents a policy provider which uses permissions
    /// </summary>
    public sealed class PermissionAuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        #region Fields

        private const string POLICY_PREFIXE = "Permission";
        private const string Scheme = JwtBearerDefaults.AuthenticationScheme;

        #endregion

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            var policy = new AuthorizationPolicyBuilder(Scheme).RequireAuthenticatedUser();

            return Task.FromResult(policy.Build());
        }

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => Task.FromResult<AuthorizationPolicy>(null);

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (!policyName.StartsWith(POLICY_PREFIXE))
                return Task.FromResult<AuthorizationPolicy>(null);

            var permissionName = policyName.Replace(POLICY_PREFIXE, string.Empty);
            var policy = new AuthorizationPolicyBuilder(Scheme);
            policy.AddRequirements(new PermissionAuthorizationRequirement(permissionName));

            return Task.FromResult(policy.Build());
        }
    }
}
