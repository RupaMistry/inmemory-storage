using Tranquillity.InMemory.Storage.Data;
using Tranquillity.InMemory.Storage.DataOperations.Contracts;
using Tranquillity.InMemory.Storage.Utility.Contracts;
using Tranquillity.InMemory.Storage.ValueContracts;
using System;
using System.Collections.Generic;

namespace Tranquillity.InMemory.Storage.DataOperations
{
    public class ProductKeyValueDataOperations : IDataOperations<Product>
    {
        private const string EmptyXMLAttribute = "xsi:nil";
        private const string SearializationFailure = "Failed to Serialize object value.";

        private readonly IKeyValueSerializer<Product> keyValueSerializer = null;
        private readonly KeyValueStoreOperations KeyValueStoreInstance = null;

        public ProductKeyValueDataOperations(IKeyValueSerializer<Product> keyValueSerializer)
        {
            this.keyValueSerializer = keyValueSerializer;
            KeyValueStoreInstance = KeyValueStoreOperations.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Put(string nameSpace, string key, Product value)
        {
            try
            {
                var serializedValue = this.keyValueSerializer.SerializeObject(value);

                if (string.IsNullOrWhiteSpace(serializedValue) || serializedValue.Contains(EmptyXMLAttribute))
                    throw new InvalidCastException(SearializationFailure);

                KeyValueStoreInstance.Put(nameSpace, key, serializedValue);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Product Get(string nameSpace, string key)
        {
            try
            {
                var value = KeyValueStoreInstance.Get(nameSpace, key);
                var product = this.keyValueSerializer.DeserializeObject(value);
                return product;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(string nameSpace, string key)
        {
            try
            {
                return KeyValueStoreInstance.Delete(nameSpace, key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<KeyValuePair<string, Product>> Values(string nameSpace)
        {
            try
            {
                var data = KeyValueStoreInstance.Values(nameSpace);

                var keyValueList = new List<KeyValuePair<string, Product>>();

                for (int i = 0; i < data.Count; i++)
                {
                    keyValueList.Add(GetProductData(data[i].Key, data[i].Value));
                }

                return keyValueList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int TotalCount()
        {
            return KeyValueStoreInstance.Count;
        }

        private KeyValuePair<string, Product> GetProductData(string key, string value)
        {
            var product = this.keyValueSerializer.DeserializeObject(value);
            var newKeyValue = new KeyValuePair<string, Product>(key, product);
            return newKeyValue;
        }
    }
}