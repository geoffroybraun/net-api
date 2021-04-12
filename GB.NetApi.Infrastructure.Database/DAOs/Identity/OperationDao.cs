using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GB.NetApi.Infrastructure.Database.DAOs.Identity
{
    /// <summary>
    /// Represents an operation as stored within a database
    /// </summary>
    [Table("OPERATIONS")]
    public sealed class OperationDao : BaseDao
    {
        [Column("name")]
        public string Name { get; set; }

        public ICollection<PermissionDao> Permissions { get; set; }
    }
}
