using GB.NetApi.Infrastructure.Database.DAOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GB.NetApi.Infrastructure.Database.Configurations
{
    /// <summary>
    /// Configure the <see cref="PersonDao"/>
    /// </summary>
    public sealed class PersonDaoConfiguration : IEntityTypeConfiguration<PersonDao>
    {
        public void Configure(EntityTypeBuilder<PersonDao> builder)
        {
            builder.HasIndex(e => new { e.Firstname, e.Lastname, e.Birthdate }).IsUnique();
        }
    }
}
