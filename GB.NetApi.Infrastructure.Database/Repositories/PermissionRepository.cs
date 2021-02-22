using GB.NetApi.Domain.Models.Entities.Identity;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using GB.NetApi.Infrastructure.Database.DAOs.Identity;
using GB.NetApi.Infrastructure.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GB.NetApi.Infrastructure.Database.Repositories
{
    public sealed class PermissionRepository : IPermissionRepository
    {
        #region Fields

        private readonly ICommonReadableRepository Repository;

        #endregion

        public PermissionRepository(ICommonReadableRepository repository) => Repository = repository ?? throw new ArgumentNullException(nameof(repository));

        public async Task<IEnumerable<Permission>> ListAsync() => await Repository.ToListAsync<PermissionDao, Permission>().ConfigureAwait(false);
    }
}
