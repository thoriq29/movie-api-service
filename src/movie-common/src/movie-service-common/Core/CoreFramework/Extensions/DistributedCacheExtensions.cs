using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Service.Core.Framework.Services;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Service.Core.Framework.Extensions
{
    public static class DistributedCacheRedisExtension
    {

        public static IEnumerable<RedisKey> GetKeys(this IDistributedCache distributedCache, IConfiguration configuration, RedisValue pattern)
        {
            IEnumerable<RedisKey> keys = Enumerable.Empty<RedisKey>();
            string conn = configuration.GetSection("RedisServer").GetValue<string>("ConnectionMultiplexer");
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(conn))
            {
                string[] redisServer = conn.Split(',');
                keys = redis.GetServer(redisServer[0]).Keys(pattern: pattern).ToList();
            }

            return keys;
        }

        public static IEnumerable<RedisKey> GetAllKeys(this IDistributedCache distributedCache, IConfiguration configuration)
        {
            return GetKeys(distributedCache, configuration, "*");
        }

        public static IEnumerable<string> GetAllKeysAsString(this IDistributedCache distributedCache, IConfiguration configuration)
        {
            return GetAllKeys(distributedCache, configuration).Select(keys => keys.ToString());
        }

        public static IEnumerable<RedisKey> GetAllAssemblyKeys(this IDistributedCache distributedCache, IConfiguration configuration, bool useUniquePrefix = true)
        {
            return useUniquePrefix ? GetAllKeys(distributedCache, configuration).Where(key => key.ToString().Contains(CachingService.CachePrefix)) : GetAllKeys(distributedCache, configuration);
        }

        public static IEnumerable<string> GetAllAssemblyKeysAsString(this IDistributedCache distributedCache, IConfiguration configuration)
        {
            return GetAllAssemblyKeys(distributedCache, configuration).Select(key => key.ToString());
        }
    }
}
