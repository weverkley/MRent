using Microsoft.EntityFrameworkCore;
using MRent.Domain.Base;
using MRent.Domain.Entities;
using MRent.Infrastructure.Configurations;

namespace MRent.Infrastructure.Contexts
{
    public class PostgresContext : DbContext
    {
        public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<MotorcycleEntity> Motorcycles { get; set; }
        public DbSet<MotorcycleLogEntity> MotorcycleLogs { get; set; }
        public DbSet<CourierEntity> Couriers { get; set; }
        public DbSet<PlanEntity> Plans { get; set; }
        public DbSet<RentEntity> Rents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MotorcycleConfiguration());
            modelBuilder.ApplyConfiguration(new MotorcycleLogConfiguration());
            modelBuilder.ApplyConfiguration(new CourierConfiguration());
            modelBuilder.ApplyConfiguration(new PlanConfiguration());
            modelBuilder.ApplyConfiguration(new RentConfiguration());
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder.Properties<DateTime>().HaveColumnType("timestamp without time zone");
            configurationBuilder.Properties<DateTime?>().HaveColumnType("timestamp with time zone");
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseAuditableEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseAuditableEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                }
                else
                {
                    Entry((BaseAuditableEntity)entityEntry.Entity).Property(p => p.CreatedAt).IsModified = false;
                }

                ((BaseAuditableEntity)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
