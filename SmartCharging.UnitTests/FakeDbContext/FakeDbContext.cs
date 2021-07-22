using SmartCharging.Data.DbContext;
using SmartCharging.Data.Entities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SmartCharging.UnitTests
{
    public class FakeDbContext : IDbContext, IUnitOfWork
    {
        private IDictionary<Type, object> _sets;

        public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            return new FakeRepository<TEntity>(this);
        }

        public int SaveChanges()
        {
            return 1;
        }

        public object Set(Type type)
        {
            _sets ??= new Dictionary<Type, object>();

            if (!_sets.TryGetValue(type, out var set))
            {
                var listType = typeof(List<>).MakeGenericType(new[] { type });
                set = (IList)Activator.CreateInstance(listType);
                _sets[type] = set;
            }

            return set;
        }
    }
}
