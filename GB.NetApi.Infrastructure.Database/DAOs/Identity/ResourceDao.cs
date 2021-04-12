using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GB.NetApi.Infrastructure.Database.DAOs.Identity
{
    /// <summary>
    /// Represents a resource as stored within a database
    /// </summary>
    [Table("RESOURCES")]
    public sealed class ResourceDao : BaseDao
    {
        [Column("name")]
        public string Name { get; set; }

        public ICollection<PermissionDao> Permissions { get; set; }
    }
}
