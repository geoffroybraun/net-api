using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GB.NetApi.Infrastructure.Database.Configurations.Identity
{
    /// <summary>
    /// Configure the <see cref="DAOs.Identity.UserClaimDao"/>
    /// </summary>
    public sealed class UserClaimDaoConfiguration : IEntityTypeConfiguration<IdentityUserClaim<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
