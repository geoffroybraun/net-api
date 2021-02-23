using GB.NetApi.Domain.Models.Entities.Identity;
using System.Threading.Tasks;

namespace GB.NetApi.Domain.Models.Interfaces.Repositories
{
    /// <summary>
    /// Represents a repository which executes an action on the <see cref="AuthenticateUser"/> entity
    /// </summary>
    public interface IAuthenticateUserRepository
    {
        /// <summary>
        /// Retrieve an <see cref="AuthenticateUser"/> entity using its name
        /// </summary>
        /// <param name="userEmail">The <see cref="AuthenticateUser"/> email address to look for</param>
        /// <returns>The found <see cref="AuthenticateUser"/> entity</returns>
        Task<AuthenticateUser> GetAsync(string userEmail);
    }
}
