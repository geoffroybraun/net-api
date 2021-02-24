using GB.NetApi.Infrastructure.Database.DAOs.Identity;

namespace GB.NetApi.Infrastructure.Database.Extensions
{
    /// <summary>
    /// Extends a <see cref="RolePermissionDao"/>
    /// </summary>
    public static class RolePermissionDaoExtension
    {
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
