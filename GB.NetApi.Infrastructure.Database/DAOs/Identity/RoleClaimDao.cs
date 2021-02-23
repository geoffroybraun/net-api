using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GB.NetApi.Infrastructure.Database.DAOs.Identity
{
    /// <summary>
    /// Represents an Identity role claim as stored within a database
    /// </summary>
    [Table("ROLE_CLAIMS")]
    public sealed class RoleClaimDao : IdentityRoleClaim<string>
    {
        [Column("id")]
        public override int Id { get => base.Id; set => base.Id = value; }

        [Column("claim_type")]
        public override string ClaimType { get => base.ClaimType; set => base.ClaimType = value; }

        [Column("claim_value")]
        public override string ClaimValue { get => base.ClaimValue; set => base.ClaimValue = value; }

        [Column("role_id")]
        public override string RoleId { get => base.RoleId; set => base.RoleId = value; }

        public RoleDao Role { get; set; }
    }
}
