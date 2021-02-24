using GB.NetApi.Infrastructure.Database.DAOs.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GB.NetApi.Infrastructure.Database.Configurations.Identity
{
    /// <summary>
    /// Configure the <see cref="PermissionDao"/>
    /// </summary>
    public sealed class PermissionDaoConfiguration : IEntityTypeConfiguration<PermissionDao>
    {
        public void Configure(EntityTypeBuilder<PermissionDao> builder)
        {
            builder.HasIndex(e => new { e.ResourceID, e.OperationID }).IsUnique();
            builder.HasMany(e => e.RolePermissions)
                .WithOne(e => e.Permission)
                .HasForeignKey(e => e.PermissionID)
                .IsRequired();
        }
    }
}
