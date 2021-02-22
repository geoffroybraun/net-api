using GB.NetApi.Domain.Models.Entities.Identity;
using GB.NetApi.Infrastructure.Database.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GB.NetApi.Infrastructure.Database.DAOs.Identity
{
    /// <summary>
    /// Represents a relationship between a <see cref="ResourceDao"/> and an <see cref="OperationDao"/> as stored within a database
    /// </summary>
    public sealed class PermissionDao : BaseReadableDao<Permission>
    {
        #region Properties

        [Column("resource_id")]
        public int ResourceID { get; set; }

        public ResourceDao Resource { get; set; }

        [Column("operation_id")]
        public int OperationID { get; set; }

        public OperationDao Operation { get; set; }

        public ICollection<RolePermissionDao> RolePermissions { get; set; }

        #endregion

        public override Permission Transform() => new Permission()
        {
            ID = ID,
            Name = this.GetName(),
            OperationID = OperationID,
            ResourceID = ResourceID
        };
    }
}
