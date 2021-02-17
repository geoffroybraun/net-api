using GB.NetApi.Infrastructure.Database.DAOs.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GB.NetApi.Infrastructure.Database.Extensions
{
    /// <summary>
    /// Extends a <see cref="ModelBuilder"/>
    /// </summary>
    public static class ModelBuilderExtension
    {
        /// <summary>
        /// Deactivate the cascading delete behavior for all models with foreign keys
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void DeactivateDeleteBehavior(this ModelBuilder modelBuilder)
        {
            var foreignKeys = modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys());

            foreach (var foreignKey in foreignKeys)
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }

        /// <summary>
        /// Configure the <see cref="RoleClaimDao"/> within the extended <see cref="ModelBuilder"/>
        /// </summary>
        /// <param name="modelBuilder">The extended <see cref="ModelBuilder"/> to configure</param>
        public static void OnRoleClaimDaoCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasKey(e => e.Id);
        }

        /// <summary>
        /// Configure the <see cref="RoleDao"/> within the extended <see cref="ModelBuilder"/>
        /// </summary>
        /// <param name="modelBuilder">The extended <see cref="ModelBuilder"/> to configure</param>
        public static void OnRoleDaoCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>(e =>
            {
                e.HasKey(e => e.Id);
                e.HasIndex(e => e.Name).IsUnique();
            });
            modelBuilder.Entity<RoleDao>()
                .HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(e => e.RoleId)
                .IsRequired();
            modelBuilder.Entity<RoleDao>()
                .HasMany(e => e.RoleClaims)
                .WithOne(e => e.Role)
                .HasForeignKey(e => e.RoleId)
                .IsRequired();
        }

        /// <summary>
        /// Configure the <see cref="UserClaimDao"/> within the extended <see cref="ModelBuilder"/>
        /// </summary>
        /// <param name="modelBuilder">The extended <see cref="ModelBuilder"/> to configure</param>
        public static void OnUserClaimDaoCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserClaim<string>>().HasKey(e => e.Id);
        }

        /// <summary>
        /// Configure the <see cref="UserDao"/> within the extended <see cref="ModelBuilder"/>
        /// </summary>
        /// <param name="modelBuilder">The extended <see cref="ModelBuilder"/> to configure</param>
        public static void OnUserDaoCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDao>().HasIndex(e => new { e.Email, e.UserName }).IsUnique();
            modelBuilder.Entity<UserDao>()
                .HasMany(e => e.UserClaims)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
            modelBuilder.Entity<UserDao>()
                .HasMany(e => e.UserLogins)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
            modelBuilder.Entity<UserDao>()
                .HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
            modelBuilder.Entity<UserDao>()
                .HasMany(e => e.UserTokens)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
        }

        /// <summary>
        /// Configure the <see cref="UserLoginDao"/> within the extended <see cref="ModelBuilder"/>
        /// </summary>
        /// <param name="modelBuilder">The extended <see cref="ModelBuilder"/> to configure</param>
        public static void OnUserLoginDaoCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(e => new { e.LoginProvider, e.ProviderKey });
        }

        /// <summary>
        /// Configure the <see cref="UserRoleDao"/> within the extended <see cref="ModelBuilder"/>
        /// </summary>
        /// <param name="modelBuilder">The extended <see cref="ModelBuilder"/> to configure</param>
        public static void OnUserRoleDaoCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(e => new { e.UserId, e.RoleId });
        }

        /// <summary>
        /// Configure the <see cref="UserTokenDao"/> within the extended <see cref="ModelBuilder"/>
        /// </summary>
        /// <param name="modelBuilder">The extended <see cref="ModelBuilder"/> to configure</param>
        public static void OnUserTokenDaoCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
        }
    }
}
