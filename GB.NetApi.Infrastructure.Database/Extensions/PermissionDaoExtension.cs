using GB.NetApi.Infrastructure.Database.DAOs.Identity;

namespace GB.NetApi.Infrastructure.Database.Extensions
{
    /// <summary>
    /// Extends a <see cref="PermissionDao"/>
    /// </summary>
    public static class PermissionDaoExtension
    {
        /// <summary>
        /// Retrieve the extended <see cref="PermissionDao"/> as a permission name
        /// </summary>
        /// <param name="permission">The extended <see cref="PermissionDao"/></param>
        /// <returns>The built permission name</returns>
        public static string GetName(this PermissionDao permission)
        {
            if (permission is null || permission.Operation is null || permission.Resource is null)
                return default;

            return $"{permission.Operation.Name}{permission.Resource.Name}";
        }
    }
}
