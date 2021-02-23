using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GB.NetApi.Infrastructure.Database.DAOs.Identity
{
    /// <summary>
    /// Represents a relationship between a <see cref="RoleDao"/> and a <see cref="PermissionDao"/> as stored within a database
    /// </summary>
    public sealed class RolePermissionDao
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("role_id")]
        public string RoleID { get; set; }

        public RoleDao Role { get; set; }

        [Column("permission_id")]
        public int PermissionID { get; set; }

        public PermissionDao Permission { get; set; }
    }
}
