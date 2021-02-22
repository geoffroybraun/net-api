using Microsoft.AspNetCore.Authorization;

namespace GB.NetApi.Application.WebApi.Authorizations
{
    /// <summary>
    /// Represents an authorization attribute which uses permissions
    /// </summary>
    public sealed class PermissionAttribute : AuthorizeAttribute
    {
        #region Fields

        private const string POLICY_PREFIXE = "Permission";

        #endregion

        public string Name
        {
            get => Policy.Replace(POLICY_PREFIXE, string.Empty);
            set => Policy = $"{POLICY_PREFIXE}{value}";
        }
    }
}
