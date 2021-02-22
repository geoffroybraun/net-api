using GB.NetApi.Domain.Models.Entities.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GB.NetApi.Domain.Models.Interfaces.Repositories
{
    /// <summary>
    /// Represents a repository which executes an action on the <see cref="Permission"/> entity
    /// </summary>
    public interface IPermissionRepository
    {
        /// <summary>
        /// Retrieve all stored <see cref="Permission"/> entities
        /// </summary>
        /// <returns>All stored <see cref="Permission"/> entities</returns>
        Task<IEnumerable<Permission>> ListAsync();
    }
}
