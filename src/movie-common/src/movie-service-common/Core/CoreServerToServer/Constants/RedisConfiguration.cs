using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServerToServer.Constants
{
    public static class RedisConfiguration
    {
        public static readonly string AccessTokenCacheName = "access-token-per-clientid";
        public static readonly string AccessTokenInstanceName = "server-to-server";
        public static readonly int RemainingAccessTokenSeconds = 60;
    }
}
