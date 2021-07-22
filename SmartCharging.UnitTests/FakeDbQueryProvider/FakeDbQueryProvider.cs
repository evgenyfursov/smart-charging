using System.Linq;
using System.Linq.Expressions;

namespace SmartCharging.UnitTests.FakeDbQueryProvider
{
    public class FakeDbQueryProvider<TEntity> : IQueryProvider
    {
        private readonly IQueryProvider _inner;

        public FakeDbQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new FakeDbEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new FakeDbEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }
    }
}
