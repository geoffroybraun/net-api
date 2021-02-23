using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GB.NetApi.Infrastructure.Database.DAOs.Identity
{
    /// <summary>
    /// Represents an identity user as stored within a database
    /// </summary>
    [Table("USERS")]
    public sealed class UserDao : IdentityUser
    {
        [Column("access_failed_count")]
        public override int AccessFailedCount { get => base.AccessFailedCount; set => base.AccessFailedCount = value; }

        [Column("concurrency_stamp")]
        public override string ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }

        [Column("email")]
        public override string Email { get => base.Email; set => base.Email = value; }

        [Column("email_confirmed")]
        public override bool EmailConfirmed { get => base.EmailConfirmed; set => base.EmailConfirmed = value; }

        [Column("id")]
        public override string Id { get => base.Id; set => base.Id = value; }

        [Column("lockout_enabled")]
        public override bool LockoutEnabled { get => base.LockoutEnabled; set => base.LockoutEnabled = value; }

        [Column("lockout_end")]
        public override DateTimeOffset? LockoutEnd { get => base.LockoutEnd; set => base.LockoutEnd = value; }

        [Column("normalized_email")]
        public override string NormalizedEmail { get => base.NormalizedEmail; set => base.NormalizedEmail = value; }

        [Column("normalized_user_name")]
        public override string NormalizedUserName { get => base.NormalizedUserName; set => base.NormalizedUserName = value; }

        [Column("password_hash")]
        public override string PasswordHash { get => base.PasswordHash; set => base.PasswordHash = value; }

        [Column("phone_number")]
        public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }

        [Column("phone_number_confirmed")]
        public override bool PhoneNumberConfirmed { get => base.PhoneNumberConfirmed; set => base.PhoneNumberConfirmed = value; }

        [Column("security_stamp")]
        public override string SecurityStamp { get => base.SecurityStamp; set => base.SecurityStamp = value; }

        [Column("two_factor_enabled")]
        public override bool TwoFactorEnabled { get => base.TwoFactorEnabled; set => base.TwoFactorEnabled = value; }

        [Column("user_name")]
        public override string UserName { get => base.UserName; set => base.UserName = value; }

        public ICollection<UserClaimDao> UserClaims { get; set; }

        public ICollection<UserLoginDao> UserLogins { get; set; }

        public ICollection<UserRoleDao> UserRoles { get; set; }

        public ICollection<UserTokenDao> UserTokens { get; set; }
    }
}
