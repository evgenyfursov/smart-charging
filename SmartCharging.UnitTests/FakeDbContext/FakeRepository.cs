using SmartCharging.Data.DbContext;
using SmartCharging.Data.Entities;
using SmartCharging.UnitTests.FakeDbQueryProvider;
using System.Collections.Generic;
using System.Linq;

namespace SmartCharging.UnitTests
{
    public class FakeRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly List<TEntity> _set;

        public FakeRepository(FakeDbContext context)
        {
            _set = (List<TEntity>)context.Set(typeof(TEntity));
        }

        public void Add(TEntity entity)
        {
            _set.Add(entity);
        }

        public IEnumerable<TEntity> Get()
        {
            return _set;
        }

        public IQueryable<TEntity> Query()
        {
            return new FakeDbEnumerable<TEntity>(_set).AsQueryable();
        }

        public void Remove(TEntity entity)
        {
            _set.Remove(entity);
        }
    }
}
