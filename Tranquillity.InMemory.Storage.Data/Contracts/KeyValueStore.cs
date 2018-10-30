using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Tranquillity.InMemory.Storage.Data.Contracts
{
    /// <summary>
    /// Core Storage for In-Memory KeyValue Dictionary
    /// </summary>
    internal static class KeyValueStore
    {
        /// <summary>
        /// Represents a thread safe NameSpace_Key and serialized value dictionary object.
        /// </summary>
        private static ConcurrentDictionary<string, string> _store;

        /// <summary>
        /// Initializes class member instances.
        /// </summary>
        static KeyValueStore()
        {
            _store = new ConcurrentDictionary<string, string>();
        }

        /// <summary>
        /// Returns the total number of key-value items.
        /// </summary>
        public static int Count
        {
            get
            {
                return _store.Count;
            }
        }

        /// <summary>
        ///  Bool representing if the given namespace_key exists or not.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Exists(string key)
        {
            return _store.ContainsKey(key);
        }

        /// <summary>
        /// Gets a specific key-value item
        /// </summary>
        /// <param name="nameSpaceKey"></param>
        /// <returns></returns>
        public static string GetValue(string nameSpaceKey)
        {
            return _store[nameSpaceKey];
        }

        /// <summary>
        /// Gets all key-value items associated with namespace
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        public static IList<KeyValuePair<string, string>> GetAllValues(string nameSpace)
        {
            if (!nameSpace.EndsWith("_"))
                throw new KeyNotFoundException();

            var nameSpaceLookup = _store.ToLookup(i => i.Key.StartsWith(nameSpace))[true];
            var matchedList = nameSpaceLookup.ToList();

            return matchedList;
        }

        /// <summary>
        /// Creates a new key-value item, if not found. If exists, updates the associated value.
        /// </summary>
        /// <param name="nameSpaceKey"></param>
        /// <param name="value"></param>
        public static void CreateOrUpdate(string nameSpaceKey, string value)
        {
            _store.AddOrUpdate(nameSpaceKey, value, (key, oldValue) => value);
        }

        /// <summary>
        /// Deletes the specified key-value item.
        /// </summary>
        /// <param name="nameSpaceKey"></param>
        /// <returns></returns>
        public static bool Delete(string nameSpaceKey)
        {
            string oldValue;
            return _store.TryRemove(nameSpaceKey, out oldValue);
        }

        /// <summary>
        /// Clears the entire store collection. This is added only for Testing purpose.
        /// </summary>
        public static void Clear()
        {
            _store.Clear();
        }
    }
}