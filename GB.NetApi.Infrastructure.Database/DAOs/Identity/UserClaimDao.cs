using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GB.NetApi.Infrastructure.Database.DAOs.Identity
{
    /// <summary>
    /// Represents an identity user claim as stored within a database
    /// </summary>
    [Table("USER_CLAIMS")]
    public sealed class UserClaimDao : IdentityUserClaim<string>
    {
        [Column("id")]
        public override int Id { get => base.Id; set => base.Id = value; }

        [Column("claim_type")]
        public override string ClaimType { get => base.ClaimType; set => base.ClaimType = value; }

        [Column("claim_value")]
        public override string ClaimValue { get => base.ClaimValue; set => base.ClaimValue = value; }

        [Column("user_id")]
        public override string UserId { get => base.UserId; set => base.UserId = value; }

        public UserDao User { get; set; }
    }
}
