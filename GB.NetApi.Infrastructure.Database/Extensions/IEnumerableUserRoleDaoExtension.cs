using GB.NetApi.Domain.Services.Extensions;
using GB.NetApi.Infrastructure.Database.DAOs.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace GB.NetApi.Infrastructure.Database.Extensions
{
    /// <summary>
    /// Extends an <see cref="IEnumerable{UserRoleDao}"/> implementation
    /// </summary>
    public static class IEnumerableUserRoleDaoExtension
    {
        /// <summary>
        /// Retrieve the extended <see cref="IEnumerable{UserRoleDao}"/> implementation as permission names
        /// </summary>
        /// <param name="userRoles">The extended <see cref="IEnumerable{UserRoleDao}"/> implementation to convert</param>
        /// <returns>The built permission names</returns>
        public static IEnumerable<string> GetPermissionNames(this IEnumerable<UserRoleDao> userRoles)
        {
            return userRoles.IsNotNullNorEmpty() ? userRoles.SelectMany(e => e.GetPermissionNames()) : default;
        }

        /// <summary>
        /// Retrieve the extended <see cref="IEnumerable{UserRoleDao}"/> implementation as <see cref="IEnumerable{Claim}"/>
        /// </summary>
        /// <param name="userRoles">The extended <see cref="IEnumerable{UserRoleDao}"/> to convert</param>
        /// <returns>The converted <see cref="IEnumerable{Claim}"/> implementation</returns>
        public static IEnumerable<Claim> ToRoleClaims(this IEnumerable<UserRoleDao> userRoles)
        {
            return userRoles.IsNotNullNorEmpty() ? userRoles.SelectMany(e => e.ToRoleClaims()) : default;
        }
    }
}
