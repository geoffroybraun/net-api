using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Interfaces.Libraries;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using GB.NetApi.Infrastructure.Database.Contexts;
using GB.NetApi.Infrastructure.Database.DAOs.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GB.NetApi.Infrastructure.Database.Repositories
{
    public sealed class AuthenticateUserRepository : BaseRepository, IAuthenticateUserRepository
    {
        public AuthenticateUserRepository(Func<BaseDbContext> contextFunction, ITaskHandler taskHandler) : base(contextFunction, taskHandler) { }

        public async Task<AuthenticateUser> GetAsync(string userName)
        {
            using (var context = ContextFunction())
            {
                Task<UserDao> function() => GetQuery<UserDao>(context)
                    .Include(e => e.UserClaims)
                    .Include(e => e.UserRoles)
                    .SingleOrDefaultAsync();

                var result = await TaskHandler.HandleAsync(function).ConfigureAwait(false);

                return new AuthenticateUser()
                {
                    ID = result.Id,
                    Name = result.UserName,
                    Claims = result.UserClaims.Select(e => e.Transform())
                };
            }
        }
    }
}
