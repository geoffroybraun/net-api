using GB.NetApi.Infrastructure.Database.DAOs.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GB.NetApi.Infrastructure.Database.Configurations.Identity
{
    /// <summary>
    /// Configure the <see cref="ResourceDao"/>
    /// </summary>
    public sealed class ResourceDaoConfiguration : IEntityTypeConfiguration<ResourceDao>
    {
        public void Configure(EntityTypeBuilder<ResourceDao> builder)
        {
            builder.HasMany(e => e.Permissions)
                .WithOne(e => e.Resource)
                .HasForeignKey(e => e.ResourceID)
                .IsRequired();
        }
    }
}
