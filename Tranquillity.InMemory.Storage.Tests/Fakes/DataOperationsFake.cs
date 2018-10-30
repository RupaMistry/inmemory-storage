using Tranquillity.InMemory.Storage.Data.Contracts;
using Tranquillity.InMemory.Storage.DataOperations.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranquillity.InMemory.Storage.Tests.Fakes
{
    public class DataOperationsFake<T> : IDataOperations<T>
    {
        internal bool IsGetFunctionCalled { get; private set; }

        internal bool IsValuesFunctionCalled { get; private set; }

        internal bool IsKeyStoreValueAdded { get; private set; }

        internal bool IsKeyStoreValueDeleted { get; private set; }

        bool IDataOperations<T>.Delete(string nameSpace, string key)
        {
            this.IsKeyStoreValueDeleted = true;
            return true;
        }

        T IDataOperations<T>.Get(string nameSpace, string key)
        {
            this.IsGetFunctionCalled = true;
            return default(T);
        }

        void IDataOperations<T>.Put(string nameSpace, string key, T value)
        {
            this.IsKeyStoreValueAdded = true;
        }

        IList<KeyValuePair<string, T>> IDataOperations<T>.Values(string nameSpace)
        {
            this.IsValuesFunctionCalled = true;
            return new List<KeyValuePair<string, T>>();
        }

        public int TotalCount()
        {
            return 0;
        }
    }
}
