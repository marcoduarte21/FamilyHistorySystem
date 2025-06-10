using FamilyHistorySystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FamilyHistorySystem.DataAccess
{
    public class DBContexto(DbContextOptions<DBContexto> options) : DbContext(options)
    {
        public DbSet<Student> Students { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<AuditableEntity>();
            foreach (var entry in entries)
            {
                var now = DateTime.UtcNow;


                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                }
                
                entry.Entity.UpdatedAt = now;

            }
            return await base.SaveChangesAsync(cancellationToken);
        }

    }


}