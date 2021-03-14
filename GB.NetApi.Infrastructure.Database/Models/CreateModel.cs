using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Infrastructure.Database.DAOs;
using System;

namespace GB.NetApi.Infrastructure.Database.Models
{
    /// <summary>
    /// Represents a model which provides a function setting a <see cref="TDao"/> properties before creating it
    /// </summary>
    /// <typeparam name="TEntity">The entity type the DAO matches</typeparam>
    /// <typeparam name="TDao">The DAO type to create</typeparam>
    public sealed record CreateModel<TEntity, TDao> where TDao : BaseWritableDao<TEntity> where TEntity : BaseStorableEntity
    {
        public Action<TDao> SetPropertiesBeforeCreate { get; init; }
    }
}
