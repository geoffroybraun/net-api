using GB.NetApi.Domain.Services.Extensions;
using GB.NetApi.Infrastructure.Database.DAOs.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace GB.NetApi.Infrastructure.Database.Extensions
{
    /// <summary>
    /// Extends an <see cref="IEnumerable{UserClaimDao}"/> implementation
    /// </summary>
    public static class UserClaimDaoExtension
    {
        /// <summary>
        /// Retrieve the extended <see cref="IEnumerable{UserClaimDao}"/> implementation as <see cref="IEnumerable{Claim}"/>
        /// </summary>
        /// <param name="userClaims">The extended <see cref="IEnumerable{UserClaimDao}"/> implementation to convert</param>
        /// <returns>The converted <see cref="IEnumerable{Claim}"/> implementation</returns>
        public static IEnumerable<Claim> ToClaims(this IEnumerable<UserClaimDao> userClaims)
        {
            return userClaims.IsNotNullNorEmpty() ? userClaims.Select(e => e.ToClaim()) : default;
        }
    }
}
