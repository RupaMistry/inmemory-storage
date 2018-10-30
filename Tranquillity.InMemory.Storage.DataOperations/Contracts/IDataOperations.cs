using System.Collections.Generic;

namespace Tranquillity.InMemory.Storage.DataOperations.Contracts
{
    /// <summary>
    /// Generic Interface for managing a custom value object for Key-Value Store Operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataOperations<T>
    {
        /// <summary>
        /// Adds or Updates a Key-Value store
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Put(string nameSpace, string key, T value);

        /// <summary>
        /// Gets a specific Key-Value store
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get(string nameSpace, string key);

        /// <summary>
        /// Deletes a specific Key-Value store
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Delete(string nameSpace, string key);

        /// <summary>
        /// Gets all Key-Values
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        IList<KeyValuePair<string, T>> Values(string nameSpace);

        /// <summary>
        /// Returns the total number of key-value items.
        /// </summary>
        /// <returns></returns>
        int TotalCount();
    }
}