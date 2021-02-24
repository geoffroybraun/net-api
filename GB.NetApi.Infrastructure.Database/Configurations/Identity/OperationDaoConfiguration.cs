using GB.NetApi.Infrastructure.Database.DAOs.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GB.NetApi.Infrastructure.Database.Configurations
{
    /// <summary>
    /// Configure the <see cref="OperationDao"/>
    /// </summary>
    public sealed class OperationDaoConfiguration : IEntityTypeConfiguration<OperationDao>
    {
        public void Configure(EntityTypeBuilder<OperationDao> builder)
        {
            builder.HasMany(e => e.Permissions)
                .WithOne(e => e.Operation)
                .HasForeignKey(e => e.OperationID)
                .IsRequired();
        }
    }
}
