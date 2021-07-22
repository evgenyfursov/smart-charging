using Microsoft.EntityFrameworkCore;
using SmartCharging.Data.Entities;

namespace SmartCharging.Data.DbContext
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext, IUnitOfWork, IDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            return new Repository<TEntity>(this);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Group>().HasKey(x => x.Id);
            builder.Entity<ChargeStation>().HasKey(x => x.Id);
            builder.Entity<Connector>().HasKey(x => new { x.Id, x.ChargeStationId });

            builder.Entity<ChargeStation>()
            .HasOne(p => p.Group)
            .WithMany(t => t.Stations)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Connector>()
            .HasOne(p => p.ChargeStation)
            .WithMany(t => t.Connectors)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
