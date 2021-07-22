using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SmartCharging.UnitTests.FakeDbQueryProvider
{
    public class FakeDbEnumerable<T> : EnumerableQuery<T>, IEnumerable<T>, IQueryable<T>
    {
        public FakeDbEnumerable(IEnumerable<T> enumerable) : base(enumerable)
        {
        }

        public FakeDbEnumerable(Expression expression) : base(expression)
        {
        }

        IQueryProvider IQueryable.Provider => new FakeDbQueryProvider<T>(this);

        public IAsyncEnumerator<T> GetEnumerator()
        {
            return new FakeDbEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }
    }
}
