using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCharging.UnitTests.FakeDbQueryProvider
{
    public class FakeDbEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public FakeDbEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public T Current => _inner.Current;

        public ValueTask<bool> MoveNextAsync()
        {
            return new ValueTask<bool>(Task.FromResult(_inner.MoveNext()));
        }

        public ValueTask DisposeAsync()
        {
            _inner.Dispose();

            return new ValueTask(Task.FromResult(0));
        }
    }
}
