using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Core.Interfaces.Health;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.MySql
{
    public class StartupMysql
    {
        private readonly IConfiguration configuration;

        public static void ConfigureServices(IServiceCollection services)
        {
            ConfigureServicesScopes(services);
        }

        private static void ConfigureServicesScopes(IServiceCollection services)
        {
            services.AddScoped<IHealthMySqlRepository<BaseModel>, HealthRepository>();
        }

        public static void NecessaryInjectionBeforeConfigured<HealthMySqlRepository>() where HealthMySqlRepository : IHealthMySqlRepository<BaseModel>
        {

        }

        public static void NecessaryInjectionAfterConfigured()
        {

        }
    }
}
