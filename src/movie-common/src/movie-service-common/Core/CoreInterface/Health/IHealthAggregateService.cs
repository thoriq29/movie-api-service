using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Service.Core.Interfaces.Log;

namespace Service.Core.Interfaces.Health
{
    public interface IHealthAggregateService
    {
        public IDbHealthService DbHealthService { get; set; }
        public ICoreLogger Logger { get; set; }
        public IHostApplicationLifetime ApplicationLifetime { get; set; }
        public IConfiguration Configuration { get; set; }
    }
}
