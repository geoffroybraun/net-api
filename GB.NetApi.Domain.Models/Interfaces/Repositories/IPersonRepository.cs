using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Entities.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GB.NetApi.Domain.Models.Interfaces.Repositories
{
    /// <summary>
    /// Represents a repository which executes an action on the <see cref="Person"/> entity
    /// </summary>
    public interface IPersonRepository
    {
        /// <summary>
        /// Create a <see cref="Person"/> entity
        /// </summary>
        /// <param name="person">The <see cref="Person"/> entity to create</param>
        /// <returns>True if the provided <see cref="Person"/> entity has been successfully created, otherwise false</returns>
        Task<bool> CreateAsync(Person person);

        /// <summary>
        /// Indicates if a <see cref="Person"/> entity already exists
        /// </summary>
        /// <param name="person">The <see cref="Person"/> entity to check</param>
        /// <returns>True if the provided <see cref="Person"/> already exists, otherwise false</returns>
        /// <remarks>
        /// Checking a <see cref="Person"/> entity existence is based on the following properties:
        ///     * <see cref="Person.Firstname"/>
        ///     * <see cref="Person.Lastname"/>
        ///     * <see cref="Person.Birthdate"/>
        /// </remarks>
        Task<bool> ExistAsync(Person person);

        /// <summary>
        /// Retrieve all matching <see cref="Person"/> entities
        /// </summary>
        /// <param name="filter">The <see cref="PersonFilter"/> entity to use when filter</param>
        /// <returns>All filtered <see cref="Person"/> entities</returns>
        Task<IEnumerable<Person>> FilterAsync(PersonFilter filter);

        /// <summary>
        /// Retrieve all stored <see cref="Person"/> entities
        /// </summary>
        /// <returns>All stored <see cref="Person"/> entities</returns>
        Task<IEnumerable<Person>> ListAsync();
    }
}
