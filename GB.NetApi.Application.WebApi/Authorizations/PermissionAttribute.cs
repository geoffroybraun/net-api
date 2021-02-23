using Microsoft.AspNetCore.Authorization;
using System;

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

        public PermissionAttribute(string permissionName)
        {
            if (string.IsNullOrEmpty(permissionName) || string.IsNullOrWhiteSpace(permissionName))
                throw new ArgumentNullException(permissionName);

            Policy = $"{POLICY_PREFIXE}{permissionName}";
        }
    }
}
