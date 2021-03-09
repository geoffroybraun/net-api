using GB.NetApi.Domain.Services.Extensions;
using GB.NetApi.Infrastructure.Database.DAOs.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace GB.NetApi.Infrastructure.Database.Extensions
{
    /// <summary>
    /// Extends an <see cref="IEnumerable{RoleClaimDao}"/> implementation
    /// </summary>
    public static class RoleClaimDaoExtension
    {
        /// <summary>
        /// Retrieve the extended <see cref="IEnumerable{RoleClaimDao}"/> implementation as <see cref="IEnumerable{Claim}"/>
        /// </summary>
        /// <param name="roleClaims">The extended <see cref="IEnumerable{RoleClaimDao}"/> implementation to convert</param>
        /// <returns>The converted <see cref="IEnumerable{Claim}"/> implementation</returns>
        public static IEnumerable<Claim> ToClaims(this IEnumerable<RoleClaimDao> roleClaims)
        {
            return roleClaims.IsNotNullNorEmpty() ? roleClaims.Select(e => e.ToClaim()) : default;
        }
    }
}
