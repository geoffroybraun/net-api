using GB.NetApi.Infrastructure.Database.DAOs;
using GB.NetApi.Infrastructure.Database.DAOs.Identity;
using GB.NetApi.Infrastructure.Database.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace GB.NetApi.Infrastructure.Database.Contexts
{
    /// <summary>
    /// Represents an abstract <see cref="DbContext"/> which provides access to DAOs
    /// </summary>
    public abstract class BaseDbContext : IdentityDbContext<IdentityUser>
    {
        #region Properties

        public DbSet<OperationDao> Operations { get; set; }

        public DbSet<PermissionDao> Permissions { get; set; }

        public DbSet<PersonDao> Persons { get; set; }

        public DbSet<ResourceDao> Resources { get; set; }

        public new DbSet<RoleDao> Roles { get; set; }

        public new DbSet<RoleClaimDao> RoleClaims { get; set; }

        public DbSet<RolePermissionDao> RolePermissions { get; set; }

        public new DbSet<UserDao> Users { get; set; }

        public new DbSet<UserClaimDao> UserClaims { get; set; }

        public new DbSet<UserLoginDao> UserLogins { get; set; }

        public new DbSet<UserRoleDao> UserRoles { get; set; }

        public new DbSet<UserTokenDao> UserTokens { get; set; }

        #endregion

        protected BaseDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
                throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.OnOperationDaoCreating();
            modelBuilder.OnPermissionDaoCreating();
            modelBuilder.OnResourceDaoCreating();
            modelBuilder.OnRoleClaimDaoCreating();
            modelBuilder.OnRoleDaoCreating();
            modelBuilder.OnRolePermissionDaoCreating();
            modelBuilder.OnUserClaimDaoCreating();
            modelBuilder.OnUserDaoCreating();
            modelBuilder.OnUserLoginDaoCreating();
            modelBuilder.OnUserRoleDaoCreating();
            modelBuilder.OnUserTokenDaoCreating();
            modelBuilder.DeactivateDeleteBehavior();
        }
    }
}
