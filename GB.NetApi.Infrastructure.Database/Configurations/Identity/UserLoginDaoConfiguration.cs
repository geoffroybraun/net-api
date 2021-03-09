using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GB.NetApi.Infrastructure.Database.Configurations.Identity
{
    /// <summary>
    /// Configure the <see cref="DAOs.Identity.UserLoginDao"/>
    /// </summary>
    public sealed class UserLoginDaoConfiguration : IEntityTypeConfiguration<IdentityUserLogin<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder)
        {
            builder.HasKey(e => new { e.LoginProvider, e.ProviderKey });
        }
    }
}
