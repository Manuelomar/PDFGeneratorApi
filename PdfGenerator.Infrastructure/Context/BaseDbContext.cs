using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using PdfGenerator.Domain.Entities;

namespace PdfGenerator.Infrastructure.Context
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

    }
    public abstract class BaseDbContext : DbContext, IDbContext
    {
        protected BaseDbContext(DbContextOptions options) : base(options)
        {

        }

        private void SetAuditEntities()
        {

            foreach (var entry in ChangeTracker.Entries<IBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:

                        entry.Entity.IsDeleted = false;
                        entry.Entity.CreatedDate = DateTimeOffset.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Property(x => x.CreatedDate).IsModified = false;
                        entry.Entity.UpdatedDate = DateTimeOffset.UtcNow;
                        break;

                    case EntityState.Deleted:
                        entry.Property(x => x.CreatedDate).IsModified = false;
                        entry.State = EntityState.Modified;
                        entry.Entity.IsDeleted = true;
                        entry.Entity.DeletedDate = DateTimeOffset.UtcNow;
                        break;

                    default:
                        break;
                }
            }
        }

        public override int SaveChanges()
        {
            SetAuditEntities();
            return base.SaveChanges();
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetAuditEntities();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            SetAuditEntities();
            return await base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IBase).IsAssignableFrom(type.ClrType))
                {
                    modelBuilder.SetSoftDeleteFilter(type.ClrType);
                }
            }
        }

    }
}
