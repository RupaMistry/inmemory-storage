using Tranquillity.InMemory.Storage.Data.Contracts;
using System;
using System.Collections.Generic;

namespace Tranquillity.InMemory.Storage.Data
{
    /// <summary>
    /// Class to handle and manage all operations for KeyValueStore
    /// </summary>
    public sealed class KeyValueStoreOperations
    {
        /// <summary>
        /// Represents the namespace and key primary format.
        /// </summary>
        private static readonly string keyFormat = "{0}_{1}";

        private static readonly Lazy<KeyValueStoreOperations> instance = new Lazy<KeyValueStoreOperations>(() => new KeyValueStoreOperations());

        /// <summary>
        /// Returns the KeyValueStoreOperations Instance
        /// </summary>
        public static KeyValueStoreOperations Instance
        {
            get { return instance.Value; }
        }

        /// <summary>
        /// Private constructor to prevent outside initialization of KeyValueStoreOperations class.
        /// </summary>
        private KeyValueStoreOperations() { }

        /// <summary>
        /// Returns the total number of key-value items.
        /// </summary>
        public int Count
        {
            get
            {
                return KeyValueStore.Count;
            }
        }

        /// <summary>
        /// Creates a new key-value item, if not found. If exists, updates the associated value.
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Put(string nameSpace, string key, string value)
        {
            string nameSpaceKey = GetNameSpaceKey(nameSpace, key);

            KeyValueStore.CreateOrUpdate(nameSpaceKey, value);
        }
        
        /// <summary>
        /// Gets a specific key-value item
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string nameSpace, string key)
        {
            string nameSpaceKey = GetNameSpaceKey(nameSpace, key);

            return KeyValueStore.GetValue(nameSpaceKey);
        }

        /// <summary>
        /// Gets all key-value items associated with namespace
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        public IList<KeyValuePair<string, string>> Values(string nameSpace)
        {
            string nameSpaceKey = GetNameSpaceKey(nameSpace, string.Empty);

            return KeyValueStore.GetAllValues(nameSpaceKey);
        }

        /// <summary>
        /// Deletes the specified key-value item.
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Delete(string nameSpace, string key)
        {
            string nameSpaceKey = string.Format(keyFormat, nameSpace, key);

            return KeyValueStore.Delete(nameSpaceKey);
        }

        /// <summary>
        /// Returns the standard NameSpace and Key format.
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetNameSpaceKey(string nameSpace, string key)
        {
            nameSpace = nameSpace.Trim().Replace(' ', '-');

            if (!string.IsNullOrEmpty(key))
                key = key.Trim().Replace(' ', '-');

            return string.Format(keyFormat, nameSpace, key);
        }
    }
}