using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GB.NetApi.Infrastructure.Database.DAOs.Identity
{
    /// <summary>
    /// Represents an identity user token as stored within a database
    /// </summary>
    [Table("USER_TOKENS")]
    public sealed class UserTokenDao : IdentityUserToken<string>
    {
        [Column("name")]
        public override string Name { get => base.Name; set => base.Name = value; }

        [Column("value")]
        public override string Value { get => base.Value; set => base.Value = value; }

        [Column("login_provider")]
        public override string LoginProvider { get => base.LoginProvider; set => base.LoginProvider = value; }

        [Column("user_id")]
        public override string UserId { get => base.UserId; set => base.UserId = value; }

        public UserDao User { get; set; }
    }
}
