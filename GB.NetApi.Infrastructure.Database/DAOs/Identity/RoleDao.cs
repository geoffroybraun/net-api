using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GB.NetApi.Infrastructure.Database.DAOs.Identity
{
    /// <summary>
    /// Represents an identity role as stored within a database
    /// </summary>
    [Table("ROLES")]
    public sealed class RoleDao : IdentityRole
    {
        [Column("id")]
        public override string Id { get => base.Id; set => base.Id = value; }

        [Column("name")]
        public override string Name { get => base.Name; set => base.Name = value; }

        [Column("Concurrency_stamp")]
        public override string ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }

        [Column("normalized_name")]
        public override string NormalizedName { get => base.NormalizedName; set => base.NormalizedName = value; }

        public ICollection<RoleClaimDao> RoleClaims { get; set; }

        public ICollection<RolePermissionDao> RolePermissions { get; set; }

        public ICollection<UserRoleDao> UserRoles { get; set; }
    }
}
