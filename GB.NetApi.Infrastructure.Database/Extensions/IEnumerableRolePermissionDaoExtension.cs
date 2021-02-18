using GB.NetApi.Domain.Services.Extensions;
using GB.NetApi.Infrastructure.Database.DAOs.Identity;
using System.Collections.Generic;
using System.Linq;

namespace GB.NetApi.Infrastructure.Database.Extensions
{
    /// <summary>
    /// Extends a <see cref="IEnumerable{RolePermissionDao}"/> implementation
    /// </summary>
    public static class IEnumerableRolePermissionDaoExtension
    {
        /// <summary>
        /// Retrieve the extended <see cref="IEnumerable{RolePermissionDao}"/> implementation as permission names
        /// </summary>
        /// <param name="rolePermissions">The extended <see cref="IEnumerable{RolePermissionDao}"/> implementation to convert</param>
        /// <returns>The built permission names</returns>
        public static IEnumerable<string> GetPermissionNames(this IEnumerable<RolePermissionDao> rolePermissions)
        {
            return rolePermissions.IsNotNullNorEmpty() ? rolePermissions.Select(e => e.GetPermissionName()) : default;
        }
    }
}
