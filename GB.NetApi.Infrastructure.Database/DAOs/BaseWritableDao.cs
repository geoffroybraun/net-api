using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Interfaces.Entities;
using System;
using System.Collections.Generic;

namespace GB.NetApi.Infrastructure.Database.DAOs
{
    /// <summary>
    /// Represents an abstract DAO which can be filled from a <see cref="TEntity"/> entity
    /// </summary>
    /// <typeparam name="TEntity">The entity type to be filled from</typeparam>
    public abstract class BaseWritableDao<TEntity> : BaseReadableDao<TEntity>, IFillable<TEntity> where TEntity : BaseStorableEntity
    {
        /// <summary>
        /// Allows the deriving class to override the method
        /// </summary>
        /// <param name="entity">The <see cref="TEntity"/> entity to be filled from</param>
        /// <returns>True if the deriving class can be filled from the provided <see cref="TEntity"/> entity, otherwise false</returns>
        public virtual bool CanBeFilled(TEntity entity) => !EqualityComparer<TEntity>.Default.Equals(entity, default);

        /// <summary>
        /// Allows the deriving class to override the method
        /// </summary>
        /// <param name="entity">The <see cref="TEntity"/> entity to be filled from</param>
        public virtual void Fill(TEntity entity)
        {
            if (!CanBeFilled(entity))
                throw new ArgumentNullException(nameof(entity));

            ID = entity.ID;
        }
    }
}
