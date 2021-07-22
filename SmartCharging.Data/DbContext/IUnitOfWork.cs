using SmartCharging.Data.Entities;

namespace SmartCharging.Data.DbContext
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        int SaveChanges();
    }
}
