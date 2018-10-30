using Tranquillity.InMemory.Storage.Business.Contracts;
using Tranquillity.InMemory.Storage.DataOperations.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tranquillity.InMemory.Storage.Business
{
    public class KeyValueStoreBusiness<T> : IStoreBusiness<T>
    {
        private readonly IDataOperations<T> dataOperation;

        public KeyValueStoreBusiness(IDataOperations<T> dataOperation)
        {
            this.dataOperation = dataOperation;
        }

        public void Put(string nameSpace, string key, T value)
        {
            if (this.ValidateParameters(nameSpace, key))
            {
                if(value == null)
                    throw new ArgumentNullException("value");

                this.dataOperation.Put(nameSpace, key, value);
            }
        }

        public T Get(string nameSpace, string key)
        {
            if (this.ValidateParameters(nameSpace, key))
            {
                return this.dataOperation.Get(nameSpace, key);
            }
            return default(T);
        }

        public bool Delete(string nameSpace, string key)
        {
            if (this.ValidateParameters(nameSpace, key))
            {
                return this.dataOperation.Delete(nameSpace, key);
            }
            return false;
        }

        public IList<KeyValuePair<string, T>> Values(string nameSpace)
        {
            if (string.IsNullOrWhiteSpace(nameSpace))
                throw new ArgumentNullException("nameSpace");

           return this.dataOperation.Values(nameSpace).ToList();
        }

        private bool ValidateParameters(string nameSpace, string key)
        {
            if (string.IsNullOrWhiteSpace(nameSpace))
                throw new ArgumentNullException("nameSpace");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException("key");

            return true;
        }
    }
}