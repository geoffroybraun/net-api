using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GB.NetApi.Infrastructure.Database.DAOs.Identity
{
    /// <summary>
    /// Represents an identity user login as stored within a database
    /// </summary>
    [Table("USER_LOGINS")]
    public sealed class UserLoginDao : IdentityUserLogin<string>
    {
        [Column("provider_key")]
        public override string ProviderKey { get => base.ProviderKey; set => base.ProviderKey = value; }

        [Column("provider_display_name")]
        public override string ProviderDisplayName { get => base.ProviderDisplayName; set => base.ProviderDisplayName = value; }

        [Column("login_provider")]
        public override string LoginProvider { get => base.LoginProvider; set => base.LoginProvider = value; }

        [Column("user_id")]
        public override string UserId { get => base.UserId; set => base.UserId = value; }

        public UserDao User { get; set; }
    }
}
