using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GB.NetApi.Infrastructure.Database.DAOs
{
    /// <summary>
    /// Represents an abstract DAO which can be transformed to a <see cref="TEntity"/> entity
    /// </summary>
    /// <typeparam name="TEntity">The entity type to be transformed to</typeparam>
    public abstract class BaseReadableDao<TEntity> : ITransformable<TEntity> where TEntity : BaseStorableEntity
    {
        #region Properties

        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        #endregion

        /// <summary>
        /// Delegate the method implementation to the deriving class
        /// </summary>
        /// <returns>The transformed deriving class</returns>
        public abstract TEntity Transform();
    }
}
