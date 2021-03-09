using GB.NetApi.Infrastructure.Database.DAOs.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GB.NetApi.Infrastructure.Database.Configurations.Identity
{
    /// <summary>
    /// Configure the <see cref="UserDao"/>
    /// </summary>
    public sealed class UserDaoConfiguration : IEntityTypeConfiguration<UserDao>
    {
        public void Configure(EntityTypeBuilder<UserDao> builder)
        {
            builder.HasIndex(e => new { e.Email, e.UserName }).IsUnique();
            builder.HasMany(e => e.UserClaims)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
            builder.HasMany(e => e.UserLogins)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
            builder.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
            builder.HasMany(e => e.UserTokens)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
        }
    }
}
