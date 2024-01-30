using Service.Core.Framework.Hosting;
using Service.Core.Framework.Program;
using System.Threading.Tasks;

namespace Movie.Api
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            var environmentName = EnvironmentName.Release;
#if DEBUG
           environmentName = EnvironmentName.Debug;
#elif DEVELOPMENT
           environmentName = EnvironmentName.Development;
#elif SANDBOX
           environmentName = EnvironmentName.Sandbox;
#elif STAGING
           environmentName = EnvironmentName.Staging;
#elif REVIEW
           environmentName = EnvironmentName.Review;
#elif TESTING
           environmentName = EnvironmentName.Testing;
#endif

            Task<int> task = BackendCoreProgram.Main<Startup>(args, environmentName, builder => {
                builder.AddAppSetting("appsettings");
            });

            return task.GetAwaiter().GetResult();
        }
    }
}