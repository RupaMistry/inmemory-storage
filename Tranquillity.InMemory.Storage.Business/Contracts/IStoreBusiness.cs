using System.Collections.Generic;

namespace Tranquillity.InMemory.Storage.Business.Contracts
{
    /// <summary>
    /// Interface for Key-Value Store Business
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStoreBusiness<T>
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
    }
}
