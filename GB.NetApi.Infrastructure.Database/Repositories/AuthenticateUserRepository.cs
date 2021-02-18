using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using GB.NetApi.Domain.Services.Extensions;
using GB.NetApi.Infrastructure.Database.DAOs.Identity;
using GB.NetApi.Infrastructure.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GB.NetApi.Infrastructure.Database.Repositories
{
    public sealed class AuthenticateUserRepository : IAuthenticateUserRepository
    {
        #region Fields

        private readonly ICommonRepository Repository;

        #endregion

        public AuthenticateUserRepository(ICommonRepository repository) => Repository = repository ?? throw new ArgumentNullException(nameof(repository));

        public async Task<AuthenticateUser> GetAsync(string userName)
        {
            using (var context = Repository.InstanciateContext())
            {
                Task<UserDao> function() => Repository.GetQuery<UserDao>(context)
                    .Include(e => e.UserClaims)
                    .Include(e => e.UserRoles)
                    .ThenInclude(e => e.Role)
                    .ThenInclude(e => e.RolePermissions)
                    .ThenInclude(e => e.Permission)
                    .ThenInclude(e => e.Operation)
                    .Include(e => e.UserRoles)
                    .ThenInclude(e => e.Role)
                    .ThenInclude(e => e.RolePermissions)
                    .ThenInclude(e => e.Permission)
                    .ThenInclude(e => e.Resource)
                    .SingleOrDefaultAsync(e => e.UserName == userName);

                var result = await Repository.ExecuteAsync(function).ConfigureAwait(false);

                return Transform(result);
            }
        }

        #region Private methods

        private static AuthenticateUser Transform(UserDao user)
        {
            if (user is null)
                return default;

            return new AuthenticateUser()
            {
                ID = user.Id,
                Name = user.UserName,
                Claims = GetClaims(user.UserClaims),
                PermissionNames = GetPermissionNames(user.UserRoles)
            };
        }

        private static IEnumerable<Claim> GetClaims(IEnumerable<UserClaimDao> userClaims)
        {
            return userClaims.IsNotNullNorEmpty() ? userClaims.Select(GetClaim) : default;
        }

        private static Claim GetClaim(UserClaimDao userClaim)
        {
            return userClaim is not null ? userClaim.ToClaim() : default;
        }

        private static IEnumerable<string> GetPermissionNames(IEnumerable<UserRoleDao> userRoles)
        {
            return userRoles.IsNotNullNorEmpty() ? userRoles.SelectMany(GetPermissionNames) : default;
        }

        private static IEnumerable<string> GetPermissionNames(UserRoleDao userRole)
        {
            return userRole is not null ? GetPermissionNames(userRole.Role) : default;
        }

        private static IEnumerable<string> GetPermissionNames(RoleDao role)
        {
            return role is not null ? GetPermissionNames(role.RolePermissions) : default;
        }

        private static IEnumerable<string> GetPermissionNames(IEnumerable<RolePermissionDao> rolePermissions)
        {
            return rolePermissions.IsNotNullNorEmpty() ? rolePermissions.Select(GetPermissionName) : default;
        }

        private static string GetPermissionName(RolePermissionDao rolePermission)
        {
            return rolePermission is not null ? GetPermissionName(rolePermission.Permission) : default;
        }

        private static string GetPermissionName(PermissionDao permission)
        {
            if (permission is null || permission.Operation is null || permission.Resource is null)
                return default;

            return permission.Name;
        }

        #endregion
    }
}
