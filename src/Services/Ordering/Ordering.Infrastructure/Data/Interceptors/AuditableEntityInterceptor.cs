using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Abstractions;

namespace Ordering.Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken)
        {
            UpdateEntites(eventData.Context);
            await base.SavingChangesAsync(eventData, result, cancellationToken); 
            
            return result;
        }

        private void UpdateEntites(DbContext? context)
        {
            if (context == null) return;

            foreach (var entry in context.ChangeTracker.Entries<IEntity>())
            {
                if(entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = "mehemet";
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEnities())
                {
                    entry.Entity.CreatedBy = "mehemet";
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }
            }
        }
    }

    public static class Extentions
    {
        public static bool HasChangedOwnedEnities(this EntityEntry entry) =>
            entry.References.Any(r => r.TargetEntry != null && r.TargetEntry.Metadata.IsOwned()
            && (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }
}
