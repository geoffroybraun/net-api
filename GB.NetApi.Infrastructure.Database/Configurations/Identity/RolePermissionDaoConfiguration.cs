using GB.NetApi.Infrastructure.Database.DAOs.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GB.NetApi.Infrastructure.Database.Configurations.Identity
{
    /// <summary>
    /// Configure the <see cref="RolePermissionDao"/>
    /// </summary>
    public sealed class RolePermissionDaoConfiguration : IEntityTypeConfiguration<RolePermissionDao>
    {
        public void Configure(EntityTypeBuilder<RolePermissionDao> builder)
        {
            builder.HasIndex(e => new { e.RoleID, e.PermissionID }).IsUnique();
        }
    }
}
