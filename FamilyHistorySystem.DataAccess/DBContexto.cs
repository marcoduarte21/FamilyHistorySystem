using FamilyHistorySystem.Models.Entities;
using FamilyHistorySystem.Models.Entities.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace FamilyHistorySystem.DataAccess
{
    public class DBContexto : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DBContexto(DbContextOptions<DBContexto> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<User> Users { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<AuditableEntity>();
            var now = DateTime.UtcNow;
            var userIdStr = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid? userId = Guid.TryParse(userIdStr, out var parsedId) ? parsedId : null;

            foreach (var entry in entries)
            {

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                    entry.Entity.CreatedBy = userId;
                }

                entry.Entity.UpdatedAt = now;
                entry.Entity.UpdatedBy = userId;
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }


}