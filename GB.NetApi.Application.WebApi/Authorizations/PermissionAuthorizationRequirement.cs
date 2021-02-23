using Microsoft.AspNetCore.Authorization;
using System;

namespace GB.NetApi.Application.WebApi.Authorizations
{
    /// <summary>
    /// Represents an authorization requirement which uses permissions
    /// </summary>
    public sealed class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        #region Properties

        public readonly string PermissionName;

        #endregion

        public PermissionAuthorizationRequirement(string permissionName)
        {
            if (string.IsNullOrEmpty(permissionName) || string.IsNullOrWhiteSpace(permissionName))
                throw new ArgumentNullException(nameof(permissionName));

            PermissionName = permissionName;
        }
    }
}
