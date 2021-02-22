using GB.NetApi.Application.Services.DTOs;
using GB.NetApi.Application.Services.Queries.Permissions;
using GB.NetApi.Domain.Models.Entities.Identity;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using GB.NetApi.Domain.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GB.NetApi.Application.Services.Handlers.Permissions
{
    /// <summary>
    /// Executes a <see cref="ListPermissionsQuery"/> query
    /// </summary>
    public sealed class ListPermissionsHandler : BaseQueryHandler<ListPermissionsQuery, IEnumerable<PermissionDto>>
    {
        #region Fields

        private readonly IPermissionRepository Repository;

        #endregion

        public ListPermissionsHandler(IPermissionRepository repository) => Repository = repository ?? throw new ArgumentNullException(nameof(repository));

        public override async Task<IEnumerable<PermissionDto>> ExecuteAsync(ListPermissionsQuery query)
        {
            var result = await Repository.ListAsync().ConfigureAwait(false);

            return Transform(result);
        }

        #region Private methods

        private static IEnumerable<PermissionDto> Transform(IEnumerable<Permission> permissions)
        {
            return permissions.IsNotNullNorEmpty() ? permissions.Select(Transform) : default;
        }

        private static PermissionDto Transform(Permission permission) => (PermissionDto)permission;

        #endregion
    }
}
