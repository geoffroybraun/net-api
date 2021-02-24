using GB.NetApi.Domain.Services.Extensions;
using GB.NetApi.Infrastructure.Database.DAOs.Identity;
using System.Collections.Generic;
using System.Linq;

namespace GB.NetApi.Infrastructure.Database.Extensions
{
    /// <summary>
    /// Extends a <see cref="RolePermissionDao"/>
    /// </summary>
    public static class RolePermissionDaoExtension
    {
        /// <summary>
        /// Retrieve the extended <see cref="IEnumerable{RolePermissionDao}"/> implementation as permission names
        /// </summary>
        /// <param name="rolePermissions">The extended <see cref="IEnumerable{RolePermissionDao}"/> implementation to convert</param>
        /// <returns>The built permission names</returns>
        public static IEnumerable<string> GetPermissionNames(this IEnumerable<RolePermissionDao> rolePermissions)
        {
            return rolePermissions.IsNotNullNorEmpty() ? rolePermissions.Select(GetPermissionName) : default;
        }

        /// <summary>
        /// Retrieve the extended <see cref="RolePermissionDao"/> as a permission name
        /// </summary>
        /// <param name="rolePermission">The extended <see cref="RolePermissionDao"/></param>
        /// <returns>The built permission name</returns>
        public static string GetPermissionName(this RolePermissionDao rolePermission)
        {
            return rolePermission is not null ? rolePermission.Permission.GetName() : default;
        }
    }
}
