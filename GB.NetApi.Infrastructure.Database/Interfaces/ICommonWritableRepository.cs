using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Infrastructure.Database.DAOs;
using System.Threading.Tasks;

namespace GB.NetApi.Infrastructure.Database.Interfaces
{
    /// <summary>
    /// Represents a common repository which provides 'write' methods
    /// </summary>
    public interface ICommonWritableRepository
    {
        /// <summary>
        /// Add the provided entity
        /// </summary>
        /// <typeparam name="TEntity">The entity type to add</typeparam>
        /// <typeparam name="TDao">The DAO type to create</typeparam>
        /// <param name="entity">The entity to add</param>
        /// <returns>True if the provided entity has been successfully added, otherwise false</returns>
        Task<bool> CreateAsync<TEntity, TDao>(TEntity entity) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity;

        /// <summary>
        /// Add the provided entity
        /// </summary>
        /// <typeparam name="TEntity">The entity type to add</typeparam>
        /// <typeparam name="TDao">The DAO type to create</typeparam>
        /// <param name="entity">The entity to add</param>
        /// <param name="expectedSavedChangesCount">The expected saved changes count to measure when adding</param>
        /// <returns>True if all expected saved changes have been successfully counted, otherwise false</returns>
        Task<bool> CreateAsync<TEntity, TDao>(TEntity entity, int expectedSavedChangesCount) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity;

        /// <summary>
        /// Delete an entity using its ID
        /// </summary>
        /// <typeparam name="TEntity">The entity type to delete</typeparam>
        /// <typeparam name="TDao">The DAO type to remove</typeparam>
        /// <param name="ID">The entity ID to use when looking for the entity to delete</param>
        /// <returns>True if the targeted entity has been successfully deleted, otherwise false</returns>
        Task<bool> DeleteAsync<TEntity, TDao>(int ID) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity;

        /// <summary>
        /// Delete an entity using its ID
        /// </summary>
        /// <typeparam name="TEntity">The entity type to delete</typeparam>
        /// <typeparam name="TDao">The DAO type to remove</typeparam>
        /// <param name="ID">The entity ID to use when looking for the entity to delete</param>
        /// <param name="expectedSavedChangesCount">The expected saved changes count to measure when deleting</param>
        /// <returns>True if the targeted entity has been successfully deleted, otherwise false</returns>
        Task<bool> DeleteAsync<TEntity, TDao>(int ID, int expectedSavedChangesCount) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity;

        /// <summary>
        /// Update the provided entity
        /// </summary>
        /// <typeparam name="TEntity">The entity type to update</typeparam>
        /// <typeparam name="TDao">The DAO type to update</typeparam>
        /// <param name="entity">The entity to update</param>
        /// <returns>True if the provided entity has been successfully updated, otherwise false</returns>
        Task<bool> UpdateAsync<TEntity, TDao>(TEntity entity) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity;

        /// <summary>
        /// Update the provided entity
        /// </summary>
        /// <typeparam name="TEntity">The entity type to update</typeparam>
        /// <typeparam name="TDao">The DAO type to update</typeparam>
        /// <param name="entity">The entity to update</param>
        /// <param name="expectedSavedChangesCount">The expected saved changes count to measure when updating</param>
        /// <returns>True if the provided entity has been successfully updated, otherwise false</returns>
        Task<bool> UpdateAsync<TEntity, TDao>(TEntity entity, int expectedSavedChangesCount) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity;
    }
}
