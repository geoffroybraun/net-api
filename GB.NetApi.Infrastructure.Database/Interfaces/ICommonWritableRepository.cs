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
        /// <typeparam name="TDao">The DAO type to create</typeparam>
        /// <param name="model">The <see cref="CreateModel{TDao}"/> to use when setting the DAO properties</param>
        /// <returns>True if all expected saved changes have been successfully counted, otherwise false</returns>
        Task<bool> CreateAsync<TDao>(CreateModel<TDao> model) where TDao : BaseDao, new();

        /// <summary>
        /// Create a new entity
        /// </summary>
        /// <typeparam name="TDao">The DAO type to create</typeparam>
        /// <param name="model">The <see cref="CreateModel{TDao}"/> to use when setting the DAO properties</param>
        /// <param name="expectedSavedChangesCount">The expected saved changes count to measure when adding</param>
        /// <returns>True if all expected saved changes have been successfully counted, otherwise false</returns>
        Task<bool> CreateAsync<TDao>(CreateModel<TDao> model, int expectedSavedChangesCount) where TDao : BaseDao, new();

        /// <summary>
        /// Delete a DAO
        /// </summary>
        /// <typeparam name="TDao">The DAO type to remove</typeparam>
        /// <param name="model">The <see cref="DeleteModel{TDao}"/> to use when deleting the DAO</param>
        /// <returns>True if the targeted DAO has been successfully deleted, otherwise false</returns>
        Task<bool> DeleteAsync<TDao>(DeleteModel<TDao> model) where TDao : BaseDao;

        /// <summary>
        /// Delete a DAO
        /// </summary>
        /// <typeparam name="TDao">The DAO type to remove</typeparam>
        /// <param name="model">The <see cref="DeleteModel{TDao}"/> to use when deleting the DAO</param>
        /// <param name="expectedSavedChangesCount">The expected saved changes count to measure when deleting</param>
        /// <returns>True if the targeted DAO has been successfully deleted, otherwise false</returns>
        Task<bool> DeleteAsync<TDao>(DeleteModel<TDao> model, int expectedSavedChangesCount) where TDao : BaseDao;

        /// <summary>
        /// Update an existing entity
        /// </summary>
        /// <typeparam name="TDao">The DAO type to create</typeparam>
        /// <param name="model">The <see cref="UpdateModel{TDao}"/> to use when setting the DAO properties</param>
        /// <returns>True if all expected saved changes have been successfully counted, otherwise false</returns>
        Task<bool> UpdateAsync<TDao>(UpdateModel<TDao> model) where TDao : BaseDao;

        /// <summary>
        /// Update an existing entity
        /// </summary>
        /// <typeparam name="TDao">The DAO type to create</typeparam>
        /// <param name="model">The <see cref="UpdateModel{TDao}"/> to use when setting the DAO properties</param>
        /// <param name="expectedSavedChangesCount">The expected saved changes count to measure when updating</param>
        /// <returns>True if all expected saved changes have been successfully counted, otherwise false</returns>
        Task<bool> UpdateAsync<TDao>(UpdateModel<TDao> model, int expectedSavedChangesCount) where TDao : BaseDao;
    }
}
