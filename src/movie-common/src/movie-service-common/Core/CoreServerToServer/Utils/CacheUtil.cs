using Microsoft.Extensions.Hosting;
using Service.ServerToServer.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServerToServer.Utils
{
    internal static class CacheUtil
    {
        internal static string FinalCacheName(string cacheName)
        {
            return $"{RedisConfiguration.AccessTokenInstanceName}:{cacheName}";
        }
    }
}
