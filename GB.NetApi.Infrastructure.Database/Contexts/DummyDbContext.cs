using GB.NetApi.Infrastructure.Database.DAOs;
using GB.NetApi.Infrastructure.Database.DAOs.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace GB.NetApi.Infrastructure.Database.Contexts
{
    /// <summary>
    /// Represents a dummy <see cref="BaseDbContext"/> implementation to use within integration tests
    /// </summary>
    public sealed class DummyDbContext : BaseDbContext
    {
        public DummyDbContext() : base(BuildOptions()) => Initialize();

        #region Private methods

        private static DbContextOptions BuildOptions()
        {
            var services = new ServiceCollection();
            var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<DummyDbContext>();
            builder.UseInMemoryDatabase("DUMMY").UseInternalServiceProvider(provider);
            builder.EnableSensitiveDataLogging(true);

            return builder.Options;
        }

        private void Initialize()
        {
            var hasCHangesToSave = false;
            hasCHangesToSave |= TryAddDao(Operations, OperationDaos);
            hasCHangesToSave |= TryAddDao(Resources, ResourceDaos);
            hasCHangesToSave |= TryAddDao(Permissions, PermissionDaos);
            hasCHangesToSave |= TryAddDao(Roles, RoleDaos);
            hasCHangesToSave |= TryAddDao(RolePermissions, RolePermissionDaos);
            hasCHangesToSave |= TryAddDao(Users, UserDaos);
            hasCHangesToSave |= TryAddDao(UserRoles, UserRoleDaos);
            hasCHangesToSave |= TryAddDao(Persons, PersonDaos);

            if (hasCHangesToSave)
                SaveChanges();
        }

        private static IEnumerable<OperationDao> OperationDaos => new[]
        {
            new OperationDao() { ID = 1, Name = "Access" },
            new OperationDao() { ID = 2, Name = "Read" },
            new OperationDao() { ID = 3, Name = "Write" },
            new OperationDao() { ID = 4, Name = "Delete" }
        };

        private static IEnumerable<ResourceDao> ResourceDaos => new[]
        {
            new ResourceDao() { ID = 1, Name = "Application" },
            new ResourceDao() { ID = 2, Name = "Person" },
        };

        private static IEnumerable<PermissionDao> PermissionDaos => new[]
        {
            new PermissionDao() { ID = 1, ResourceID = 1, OperationID = 1 },
            new PermissionDao() { ID = 2, ResourceID = 2, OperationID = 2 },
            new PermissionDao() { ID = 3, ResourceID = 2, OperationID = 3 },
            new PermissionDao() { ID = 4, ResourceID = 2, OperationID = 4 },
        };

        private static IEnumerable<RoleDao> RoleDaos => new[]
        {
            new RoleDao() { Id = "guests", Name = "Guests", NormalizedName = "GUESTS" },
            new RoleDao() { Id = "readers", Name = "Readers", NormalizedName = "READERS" },
            new RoleDao() { Id = "writers", Name = "Writers", NormalizedName = "WRITERS" },
        };

        private static IEnumerable<RolePermissionDao> RolePermissionDaos => new[]
        {
            new RolePermissionDao() { ID = 1, RoleID = "Guests", PermissionID = 1 },
            new RolePermissionDao() { ID = 2, RoleID = "Readers", PermissionID = 2 },
            new RolePermissionDao() { ID = 5, RoleID = "Writers", PermissionID = 3 },
            new RolePermissionDao() { ID = 6, RoleID = "Writers", PermissionID = 4 },
        };

        private static IEnumerable<UserDao> UserDaos => new[]
        {
            new UserDao()
            {
                Id = "reader",
                UserName = "Reader",
                NormalizedUserName = "READER",
                PasswordHash = PasswordHasher.HashPassword(null, "reader"),
                Email = "reader@localhost.com",
                NormalizedEmail = "READER@LOCALHOST.COM",
                EmailConfirmed = true
            },
            new UserDao()
            {
                Id = "writer",
                UserName = "Writer",
                NormalizedUserName = "WRITER",
                PasswordHash = PasswordHasher.HashPassword(null, "writer"),
                Email = "writer@localhost.com",
                NormalizedEmail = "WRITER@LOCALHOST.COM",
                EmailConfirmed = true
            },
        };

        private static IEnumerable<UserRoleDao> UserRoleDaos => new[]
        {
            new UserRoleDao() { UserId = "reader", RoleId = "guests" },
            new UserRoleDao() { UserId = "reader", RoleId = "readers" },
            new UserRoleDao() { UserId = "writer", RoleId = "guests" },
            new UserRoleDao() { UserId = "writer", RoleId = "readers" },
            new UserRoleDao() { UserId = "writer", RoleId = "writers" },
        };

        private static IEnumerable<PersonDao> PersonDaos => new[]
        {
            new PersonDao() { ID = 1, Birthdate = new DateTime(1990, 1, 1), Firstname = "Firstname", Lastname = "Lastname" },
        };

        #endregion
    }
}
