using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Infrastructure.Database.DAOs;
using System;

namespace GB.NetApi.Infrastructure.Database.Models
{
    /// <summary>
    /// Represents a model which provides a function setting a <see cref="TDao"/> properties before updating it
    /// </summary>
    /// <typeparam name="TEntity">The entity type the DAO matches</typeparam>
    /// <typeparam name="TDao">The DAO type to update</typeparam>
    public sealed record UpdateModel<TEntity, TDao> where TDao : BaseWritableDao<TEntity> where TEntity : BaseStorableEntity
    {
        public int ID { get; init; }

        public Action<TDao> SetPropertiesBeforeUpdate { get; init; }
    }
}
