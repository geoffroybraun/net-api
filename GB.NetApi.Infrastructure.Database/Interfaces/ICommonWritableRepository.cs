using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Infrastructure.Database.DAOs;
using System.Threading.Tasks;

namespace GB.NetApi.Infrastructure.Database.Interfaces
{
    /// <summary>
    /// Represents a common repository which provides 'write' methods
    /// </summary>
    public interface ICommonWritableRepository : ICommonReadableRepository
    {
        /// <summary>
        /// Add the provided <see cref="TEntity"/> entity while counting all saved changes
        /// </summary>
        /// <param name="entity">The <see cref="TEntity"/> entity to add</param>
        /// <returns>True if all expected changes have been saved, otherwise false</returns>
        Task<bool> CreateAsync<TEntity, TDao>(TEntity entity) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity;

        /// <summary>
        /// Add the provided <see cref="TEntity"/> entity while counting all saved changes
        /// </summary>
        /// <param name="entity">The <see cref="TEntity"/> entity to add</param>
        /// <param name="expectedSaveChangesCount">The expected saved changes count when creating the <see cref="TEntity"/> entity</param>
        /// <returns>True if all expected changes have been saved, otherwise false</returns>
        Task<bool> CreateAsync<TEntity, TDao>(TEntity entity, int expectedSaveChangesCount) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity;

        /// <summary>
        /// Delete a stored entity using its ID
        /// </summary>
        /// <param name="ID">The entity ID to delete</param>
        /// <returns>True if all expected changes have been saved when deleting the entity, otherwise false</returns>
        Task<bool> DeleteAsync<TEntity, TDao>(int ID) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity;

        /// <summary>
        /// Delete a stored entity using its ID
        /// </summary>
        /// <param name="ID">The entity ID to delete</param>
        /// <param name="expectedSavedChangesCount">The expected changes count when deleting the entity</param>
        /// <returns>True if all expected changes have been saved when deleting the entity, otherwise false</returns>
        Task<bool> DeleteAsync<TEntity, TDao>(int ID, int expectedSavedChangesCount) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity;

        /// <summary>
        /// Update the stored <see cref="TEntity"/> using the provided one
        /// </summary>
        /// <param name="entity">The <see cref="TEntity"/> entity to update</param>
        /// <returns>True if the stored <see cref="TEntity"/> entity has been successfully updated using the provided one, otherwise false</returns>
        Task<bool> UpdateAsync<TEntity, TDao>(TEntity entity) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity;

        /// <summary>
        /// Update the stored <see cref="TEntity"/> entity using the provided one
        /// </summary>
        /// <param name="entity">The <see cref="TEntity"/> entity to update</param>
        /// <param name="expectedSavedChangesCount">The expected saved changes count when updating the <see cref="TEntity"/> entity</param>
        /// <returns>True if all expected changes have been saved, otherwise false</returns>
        Task<bool> UpdateAsync<TEntity, TDao>(TEntity entity, int expectedSavedChangesCount) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity;
    }
}
