using GB.NetApi.Application.Services.DTOs;
using GB.NetApi.Application.Services.Interfaces.Queries;
using System.Collections.Generic;

namespace GB.NetApi.Application.Services.Queries.Permissions
{
    /// <summary>
    /// Represents a query to retrieve all stored <see cref="PermissionDto"/>
    /// </summary>
    public sealed record ListPermissionsQuery : IQuery<IEnumerable<PermissionDto>> { }
}
