using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GB.NetApi.Infrastructure.Database.DAOs.Identity
{
    /// <summary>
    /// Represents an identity user role as stored within a database
    /// </summary>
    [Table("USERS_ROLES")]
    public sealed class UserRoleDao : IdentityUserRole<string>
    {
        [Column("user_id")]
        public override string UserId { get => base.UserId; set => base.UserId = value; }

        public UserDao User { get; set; }

        [Column("role_id")]
        public override string RoleId { get => base.RoleId; set => base.RoleId = value; }

        public RoleDao Role { get; set; }
    }
}
