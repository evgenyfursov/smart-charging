using SmartCharging.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SmartCharging.Data.DbContext
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        void Add(TEntity entity);
        IEnumerable<TEntity> Get();
        IQueryable<TEntity> Query();
        void Remove(TEntity entity);
    }
}
