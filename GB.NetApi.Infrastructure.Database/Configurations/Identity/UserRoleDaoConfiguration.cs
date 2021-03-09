using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GB.NetApi.Infrastructure.Database.Configurations.Identity
{
    /// <summary>
    /// Configure the <see cref="DAOs.Identity.UserRoleDao"/>
    /// </summary>
    public sealed class UserRoleDaoConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasKey(e => new { e.UserId, e.RoleId });
        }
    }
}
