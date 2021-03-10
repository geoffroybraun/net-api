using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Infrastructure.Database.DAOs;
using GB.NetApi.Infrastructure.Database.Models;
using System.Threading.Tasks;

namespace GB.NetApi.Infrastructure.Database.Interfaces
{
    /// <summary>
    /// Represents a common repository which provides 'write' methods
    /// </summary>
    public interface ICommonWritableRepository
    {
        /// <summary>
        /// Create a new entity
        /// </summary>
        /// <typeparam name="TEntity">The entity type to add</typeparam>
        /// <typeparam name="TDao">The DAO type to create</typeparam>
        /// <param name="model">The <see cref="CreateModel{TEntity, TDao}"/> to use when setting the DAO properties</param>
        /// <returns>True if all expected saved changes have been successfully counted, otherwise false</returns>
        Task<bool> CreateAsync<TEntity, TDao>(CreateModel<TEntity, TDao> model) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity;

        /// <summary>
        /// Create a new entity
        /// </summary>
        /// <typeparam name="TEntity">The entity type to add</typeparam>
        /// <typeparam name="TDao">The DAO type to create</typeparam>
        /// <param name="model">The <see cref="CreateModel{TEntity, TDao}"/> to use when setting the DAO properties</param>
        /// <param name="expectedSavedChangesCount">The expected saved changes count to measure when adding</param>
        /// <returns>True if all expected saved changes have been successfully counted, otherwise false</returns>
        Task<bool> CreateAsync<TEntity, TDao>(CreateModel<TEntity, TDao> model, int expectedSavedChangesCount) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity;

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
        /// Update an existing entity
        /// </summary>
        /// <typeparam name="TEntity">The entity type to add</typeparam>
        /// <typeparam name="TDao">The DAO type to create</typeparam>
        /// <param name="model">The <see cref="UpdateModel{TEntity, TDao}"/> to use when setting the DAO properties</param>
        /// <returns>True if all expected saved changes have been successfully counted, otherwise false</returns>
        Task<bool> UpdateAsync<TEntity, TDao>(UpdateModel<TEntity, TDao> model) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity;

        /// <summary>
        /// Update an existing entity
        /// </summary>
        /// <typeparam name="TEntity">The entity type to add</typeparam>
        /// <typeparam name="TDao">The DAO type to create</typeparam>
        /// <param name="model">The <see cref="UpdateModel{TEntity, TDao}"/> to use when setting the DAO properties</param>
        /// <param name="expectedSavedChangesCount">The expected saved changes count to measure when adding</param>
        /// <returns>True if all expected saved changes have been successfully counted, otherwise false</returns>
        Task<bool> UpdateAsync<TEntity, TDao>(UpdateModel<TEntity, TDao> model, int expectedSavedChangesCount) where TDao : BaseWritableDao<TEntity>, new() where TEntity : BaseStorableEntity;
    }
}
