using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GB.NetApi.Infrastructure.Database.Extensions
{
    /// <summary>
    /// Extends a <see cref="ModelBuilder"/>
    /// </summary>
    public static class ModelBuilderExtension
    {
        /// <summary>
        /// Deactivate the cascading delete behavior for all models with foreign keys
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void DeactivateDeleteBehavior(this ModelBuilder modelBuilder)
        {
            var foreignKeys = modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys());

            foreach (var foreignKey in foreignKeys)
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
