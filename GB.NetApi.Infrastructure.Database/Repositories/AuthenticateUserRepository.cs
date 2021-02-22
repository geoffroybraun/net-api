using GB.NetApi.Domain.Models.Entities.Identity;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using GB.NetApi.Infrastructure.Database.Contexts;
using GB.NetApi.Infrastructure.Database.DAOs.Identity;
using GB.NetApi.Infrastructure.Database.Extensions;
using GB.NetApi.Infrastructure.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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
                Task<UserDao> function() => GetAsync(context, userName);
                var result = await Repository.ExecuteAsync(function).ConfigureAwait(false);

                return Transform(result);
            }
        }

        #region Private methods

        private async Task<UserDao> GetAsync(BaseDbContext context, string userName) => await Repository.GetQuery<UserDao>(context)
            .Include(e => e.UserClaims)
            .Include(e => e.UserRoles)
            .ThenInclude(e => e.Role)
            .ThenInclude(e => e.RoleClaims)
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
            .SingleOrDefaultAsync(e => e.UserName == userName)
            .ConfigureAwait(false);

        private static AuthenticateUser Transform(UserDao user)
        {
            if (user is null)
                return default;

            var claims = user.UserRoles.ToRoleClaims().Union(user.UserClaims.ToClaims());
            var permissions = user.UserRoles.GetPermissionNames();

            return new AuthenticateUser()
            {
                ID = user.Id,
                Name = user.UserName,
                Claims = claims,
                PermissionNames = permissions
            };
        }

        #endregion
    }
}
