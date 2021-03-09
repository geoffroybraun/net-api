using GB.NetApi.Infrastructure.Database.DAOs.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GB.NetApi.Infrastructure.Database.Configurations.Identity
{
    /// <summary>
    /// Configure the <see cref="RoleDao"/>
    /// </summary>
    public sealed class RoleDaoConfiguration : IEntityTypeConfiguration<IdentityRole>, IEntityTypeConfiguration<RoleDao>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasKey(e => e.Id);
        }

        public void Configure(EntityTypeBuilder<RoleDao> builder)
        {
            builder.HasIndex(e => e.Name).IsUnique();
            builder.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(e => e.RoleId)
                .IsRequired();
            builder.HasMany(e => e.RoleClaims)
                .WithOne(e => e.Role)
                .HasForeignKey(e => e.RoleId)
                .IsRequired();
            builder.HasMany(e => e.RolePermissions)
                .WithOne(e => e.Role)
                .HasForeignKey(e => e.RoleID)
                .IsRequired();
        }
    }
}
