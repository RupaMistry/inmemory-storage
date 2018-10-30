using Tranquillity.InMemory.Storage.Business.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranquillity.InMemory.Storage.Tests.Fakes
{
    public class KeyValueStoreBusinessFake<T> : IStoreBusiness<T>
    {
        internal bool IsPutCalled { get; private set; }

        internal bool IsGetCalled { get; private set; }

        internal bool IsValuesCalled { get; private set; }

        internal bool IsDeleteCalled { get; private set; }

        public void Put(string nameSpace, string key, T value)
        {
            this.IsPutCalled = true;
        }

        public T Get(string nameSpace, string key)
        {
            this.IsGetCalled = true;
            return default(T);
        }

        public IList<KeyValuePair<string, T>> Values(string nameSpace)
        {
            this.IsValuesCalled = true;
            return new List<KeyValuePair<string, T>>();
        }

        public bool Delete(string nameSpace, string key)
        {
            this.IsDeleteCalled = true;
            return true;
        }
    }
}
