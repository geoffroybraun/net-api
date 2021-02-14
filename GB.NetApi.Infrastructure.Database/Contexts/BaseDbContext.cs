using GB.NetApi.Infrastructure.Database.DAOs;
using Microsoft.EntityFrameworkCore;

namespace GB.NetApi.Infrastructure.Database.Contexts
{
    /// <summary>
    /// Represents an abstract <see cref="DbContext"/> which provides access to DAOs
    /// </summary>
    public abstract class BaseDbContext : DbContext
    {
        #region Properties

        public virtual DbSet<PersonDao> Persons { get; set; }

        #endregion

        protected BaseDbContext() { }

        protected BaseDbContext(DbContextOptions options) : base(options) { }
    }
}
