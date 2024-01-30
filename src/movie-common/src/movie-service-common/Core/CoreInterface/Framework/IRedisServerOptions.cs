namespace Service.Core.Interfaces.Framework
{
    public interface IRedisServerOptions
    {
        public string CacheConfiguration { get; set; }
        public string InstanceName { get; set; }
        public bool Enabled { get; set; }
    }
}
