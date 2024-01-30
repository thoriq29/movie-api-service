using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.Interfaces.Caching
{
    public interface ICachingService
    {
        /// <summary>
        /// Store data into Redis, this method convert data into serialized.
        /// </summary>
        /// <param name="cacheName">Cache Key to store data</param>
        /// <param name="cacheContent">Data that will be stored to Redis</param>
        /// <param name="cacheDurationSec">Duration (Seconds) of data that will be stored in Redis</param>
        /// <param name="useUniquePrefix">Prefix use to add prefix on cacheName based on the service that access this method. e.g : ExampleService_CacheName</param>
        public Task SetCache<CacheContentType>(string cacheName, CacheContentType cacheContent, int cacheDurationSec, bool useUniquePrefix = true);

        /// <summary>
        /// Store data into Redis, this method convert data into serialized.
        /// </summary>
        /// <param name="cacheName">Cache Key to store data</param>
        /// <param name="cacheContent">Data that will be stored to Redis</param>
        /// <param name="cacheDurationSec">Duration (Seconds) of data that will be stored in Redis</param>
        /// <param name="useUniquePrefix">Prefix use to add prefix on cacheName based on the service that access this method. e.g : ExampleService_CacheName</param>
        /// <param name="instanceName">Override the instance name of The Redis</param>
        public Task SetCache<CacheContentType>(string cacheName, CacheContentType cacheContent, int cacheDurationSec, string instanceName, bool useUniquePrefix = true);

        /// <summary>
        /// Get data from Redis.
        /// </summary>
        /// <param name="cacheName">Cache Key to get data</param>
        /// <param name="useUniquePrefix">Prefix use to add prefix on cacheName based on the service that access this method. e.g : ExampleService_CacheName</param>
        /// <returns>Return deserialize data based on type.</returns>
        public Task<DeserializedReturnType> GetCache<DeserializedReturnType>(string cacheName, bool useUniquePrefix = true);

        /// <summary>
        /// Get data from Redis.
        /// </summary>
        /// <param name="cacheName">Cache Key to get data</param>
        /// <param name="useUniquePrefix">Prefix use to add prefix on cacheName based on the service that access this method. e.g : ExampleService_CacheName</param>
        /// <param name="instanceName">Override the instance name of The Redis</param>
        /// <returns>Return deserialize data based on type.</returns>
        public Task<DeserializedReturnType> GetCache<DeserializedReturnType>(string cacheName, string instanceName, bool useUniquePrefix = true);

        /// <summary>
        /// Get data from Redis.
        /// </summary>
        /// <param name="cacheName">Cache Key to get data</param>
        /// <param name="useUniquePrefix">Prefix use to add prefix on cacheName based on the service that access this method. e.g : ExampleService_CacheName</param>
        /// <returns>Return string of serialized data.</returns>
        public Task<string> GetCache(string cacheName, bool useUniquePrefix = true);

        /// <summary>
        /// Get data from Redis.
        /// </summary>
        /// <param name="cacheName">Cache Key to get data</param>
        /// <param name="useUniquePrefix">Prefix use to add prefix on cacheName based on the service that access this method. e.g : ExampleService_CacheName</param>
        /// <param name="instanceName">Override the instance name of The Redis</param>
        /// <returns>Return string of serialized data.</returns>
        public Task<string> GetCache(string cacheName, string instanceName, bool useUniquePrefix = true);

        /// <summary>
        /// Delete data from Redis.
        /// </summary>
        /// <param name="cacheName">Cache Key to delete data</param>
        /// <param name="useUniquePrefix">Prefix use to add prefix on cacheName based on the service that access this method. e.g : ExampleService_CacheName</param>
        /// <returns>Return string of serialized data.</returns>
        public Task DeleteCacheData(string leaderboardName, bool useUniquePrefix = true);

        /// <summary>
        /// Delete data from Redis.
        /// </summary>
        /// <param name="cacheName">Cache Key to delete data</param>
        /// <param name="useUniquePrefix">Prefix use to add prefix on cacheName based on the service that access this method. e.g : ExampleService_CacheName</param>
        /// <param name="instanceName">Override the instance name of The Redis</param>
        /// <returns>Return string of serialized data.</returns>
        public Task DeleteCacheData(string leaderboardName, string instanceName, bool useUniquePrefix = true);
    }
}
