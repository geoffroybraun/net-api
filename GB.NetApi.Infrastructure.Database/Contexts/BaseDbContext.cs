using GB.NetApi.Infrastructure.Database.DAOs;
using GB.NetApi.Infrastructure.Database.DAOs.Identity;
using GB.NetApi.Infrastructure.Database.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GB.NetApi.Infrastructure.Database.Contexts
{
    /// <summary>
    /// Represents an abstract <see cref="DbContext"/> which provides access to DAOs
    /// </summary>
    public abstract class BaseDbContext : IdentityDbContext<IdentityUser>
    {
        #region Properties

        protected static readonly IPasswordHasher<UserDao> PasswordHasher = new PasswordHasher<UserDao>();

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            builder.OnOperationDaoCreating();
            builder.OnPermissionDaoCreating();
            builder.OnResourceDaoCreating();
            builder.OnRoleClaimDaoCreating();
            builder.OnRoleDaoCreating();
            builder.OnRolePermissionDaoCreating();
            builder.OnUserClaimDaoCreating();
            builder.OnUserDaoCreating();
            builder.OnUserLoginDaoCreating();
            builder.OnUserRoleDaoCreating();
            builder.OnUserTokenDaoCreating();
            builder.DeactivateDeleteBehavior();
        }

        protected static bool TryAddDao<TDao>(DbSet<TDao> dbSet, IEnumerable<TDao> daos) where TDao : class
        {
            if (dbSet.Any())
                return false;

            dbSet.AddRange(daos);

            return true;
        }
    }
}
