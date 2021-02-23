using GB.NetApi.Infrastructure.Database.DAOs.Identity;
using System.Collections.Generic;
using System.Security.Claims;

namespace GB.NetApi.Infrastructure.Database.Extensions
{
    /// <summary>
    /// Extends a <see cref="RoleDao"/>
    /// </summary>
    public static class RoleDaoExtension
    {
        /// <summary>
        /// Retrieve the extended <see cref="RoleDao"/> as permission names
        /// </summary>
        /// <param name="role">The extended <see cref="RoleDao"/></param>
        /// <returns>The built permission names</returns>
        public static IEnumerable<string> GetPermissionNames(this RoleDao role)
        {
            return role is not null ? role.RolePermissions.GetPermissionNames() : default;
        }

        /// <summary>
        /// Retrieve the extended <see cref="RoleDao.RoleClaims"/> as <see cref="IEnumerable{Claim}"/>
        /// </summary>
        /// <param name="role">The extended <see cref="RoleDao"/> to convert</param>
        /// <returns>The converted <see cref="IEnumerable{Claim}"/> implementation</returns>
        public static IEnumerable<Claim> ToRoleClaims(this RoleDao role)
        {
            return role is not null ? role.RoleClaims.ToClaims() : default;
        }
    }
}
