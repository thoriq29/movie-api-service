using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Service.Core.Framework.Constants;
using Service.Core.Framework.Extensions;
using Service.Core.Interfaces.Caching;
using Service.Core.Interfaces.Log;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.Framework.Services
{
    public class CachingService : ICachingService
    {
        private IDistributedCache _cache;
        private readonly ICoreLogger _logger;
        private readonly IErrorFactory _errorFactory;
        private readonly IConfiguration _configuration;
        public static readonly string CachePrefix = System.Reflection.Assembly.GetEntryAssembly()?.GetName().Name + "_";
        public static readonly string CacheSeparatorIdentifier = "^";
        public RedisCacheOptions _redisCacheOptions;
        public CachingService(IDistributedCache cache, ICoreLogger logger, IErrorFactory errorFactory, IConfiguration configuration, IOptions<RedisCacheOptions> redisCacheOptions)
        {
            _cache = cache;
            _logger = logger;
            _errorFactory = errorFactory;
            _configuration = configuration;
            _redisCacheOptions = redisCacheOptions.Value;
        }
        
        public async Task SetCache<CacheContentType>(string cacheName, CacheContentType cacheContent, int cacheDurationSec = CachingConstants.DEFAULT_CACHE_DURATION, bool useUniquePrefix = true)
        {
            try
            {
                cacheName = GetCacheName(cacheName, useUniquePrefix);

                await _cache.SetStringAsync(
                    cacheName,
                    JsonConvert.SerializeObject(cacheContent),
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheDurationSec),
                        SlidingExpiration = TimeSpan.FromSeconds(cacheDurationSec)
                    }
                );

            }
            catch (Exception ex)
            {
                _logger.LogError(
                    _errorFactory.CreateError("100111", "Set Cache Failed because an exception has occurred."),
                    "Cache setting operation cannot be executed because an exception has occurred.",
                    $"cacheString: {cacheName}",
                    ex
                );
            }
        }

        public async Task SetCache<CacheContentType>(string cacheName, CacheContentType cacheContent, int cacheDurationSec, string instanceName, bool useUniquePrefix = true)
        {

            SetRedisCacheWithInstanceName(instanceName);
            await SetCache(cacheName, cacheContent, cacheDurationSec, useUniquePrefix);
        }

        public async Task<string> GetCache(string cacheName, bool useUniquePrefix = true)
        {
            cacheName = GetCacheName(cacheName, useUniquePrefix);
            return await _cache.GetStringAsync(cacheName);
        }
        
        public async Task<string> GetCache(string cacheName, string instanceName, bool useUniquePrefix = true)
        {
            SetRedisCacheWithInstanceName(instanceName);
            return await GetCache(cacheName, useUniquePrefix);
        }

        public async Task<DeserializedReturnType> GetCache<DeserializedReturnType>(string cacheName, bool useUniquePrefix = true)
        {
            cacheName = GetCacheName(cacheName, useUniquePrefix);
            var result = await GetCache(cacheName);
            if (string.IsNullOrEmpty(result))
            {
                return default;
            }

            return JsonConvert.DeserializeObject<DeserializedReturnType>(result);
        }

        public async Task<DeserializedReturnType> GetCache<DeserializedReturnType>(string cacheName, string instanceName, bool useUniquePrefix = true)
        {
            SetRedisCacheWithInstanceName(instanceName);
            return await GetCache<DeserializedReturnType>(cacheName, useUniquePrefix);
        }

        public async Task DeleteCacheData(string cacheName, bool useUniquePrefix = true)
        {
            var cacheNameList = _cache.GetAllAssemblyKeys(_configuration, useUniquePrefix);
            foreach (string cache in cacheNameList)
            {
                var getCacheName = GetCacheName(cacheName, useUniquePrefix);
                if (cache.Contains(getCacheName))
                {
                    try
                    {
                        var str = cache.Substring(cache.LastIndexOf(CacheSeparatorIdentifier) + 1);
                        await _cache.RemoveAsync(CacheSeparatorIdentifier + str);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(
                            _errorFactory.CreateError("100112", "Delete Cache Failed because an exception has occurred"),
                            "Cache setting operation cannot be executed because an exception has occurred.",
                            $"cacheString: {cache}",
                            ex
                        );
                    }
                }
            }
        }

        public async Task DeleteCacheData(string cacheName, string instanceName, bool useUniquePrefix = true)
        {
            SetRedisCacheWithInstanceName(instanceName);
            await DeleteCacheData(cacheName, useUniquePrefix);
        }

        private static string? GetCacheName(string cacheName, bool useUniquePrefix = true)
        {
            return (CacheSeparatorIdentifier + (useUniquePrefix ? CachePrefix + cacheName : cacheName));
        }

        private void SetRedisCacheWithInstanceName(string instanceName)
        {
            _redisCacheOptions.InstanceName = instanceName;
            _cache = new RedisCache(_redisCacheOptions);
        }
    }
}
