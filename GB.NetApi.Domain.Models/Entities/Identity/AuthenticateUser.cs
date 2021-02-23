using System.Collections.Generic;
using System.Security.Claims;

namespace GB.NetApi.Domain.Models.Entities.Identity
{
    /// <summary>
    /// Represents a user with both its claims and permission names lists
    /// </summary>
    public sealed record AuthenticateUser
    {
        public string ID { get; init; }

        public string Name { get; init; }

        public IEnumerable<Claim> Claims { get; init; }

        public IEnumerable<string> PermissionNames { get; init; }
    }
}
