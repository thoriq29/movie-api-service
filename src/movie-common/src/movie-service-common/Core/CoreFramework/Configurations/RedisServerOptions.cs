using Service.Core.Interfaces.Framework;

namespace Service.Core.Framework.Configurations
{
    public class RedisServerOptions : IRedisServerOptions
    {
        public string CacheConfiguration { get; set; }
        public string InstanceName { get; set; }
        public bool Enabled { get; set; }
    }
}
