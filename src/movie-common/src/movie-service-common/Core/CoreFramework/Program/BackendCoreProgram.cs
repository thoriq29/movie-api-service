using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Service.Core.Framework.Hosting;
using Service.Core.Interfaces.Framework;

namespace Service.Core.Framework.Program
{
    public static class BackendCoreProgram
    {
        private static string _environment;
        public static string environmentValue { get { return _environment; } }
        internal static string environment
        {
            get { return _environment; }
            set
            {
                switch (value)
                {
                    case "Debug":
                        _environment = value;
                        break;
                    case "Development":
                        _environment = value;
                        break;
                    case "Release":
                        _environment = value;
                        break;
                    case "Review":
                        _environment = value;
                        break;
                    case "Sandbox":
                        _environment = value;
                        break;
                    case "Staging":
                        _environment = value;
                        break;
                    case "Testing":
                        _environment = value;
                        break;
                    default:
                        _environment = "Release";
                        break;
                }
            }
        }
        public static async Task<int> Main<TStartup>(string[] args, string environmentName, Action<BackendCoreAppConfigBuilder> builder = null) where TStartup : class
        {
            try
            {
                Console.WriteLine("Welcome to Mythic Core");
                Console.WriteLine($"Environment Requested : {environmentName}");

                Console.WriteLine("Iniating Web Host...");

                Console.WriteLine("-> Configure Web Hosts initiated...");

                environment = environmentName;

                var appConfigBuilder = new BackendCoreAppConfigBuilder();
                if (builder != null) builder(appConfigBuilder);

                var host = WebHost.CreateWebHostBuilder<TStartup>(args, appConfigBuilder.Config).Build();
                Console.WriteLine("-> Configure Web Hosts finished.");

                Console.WriteLine("-> Starting server...");
                using (var scope = host.Services.CreateScope())
                {
                    Console.WriteLine("-> Pre-Run Initializing...");
                    var services = scope.ServiceProvider.GetServices<IServerPreRun>();
                    foreach (var service in services)
                    {
                        Console.WriteLine($"  => Initializing Service @ {service.GetType().ToString()}");
                        await service.Execute();
                        Console.WriteLine($"  => Done.");
                    }
                    Console.WriteLine("-> Pre-Run Initialized.");
                }

                Console.WriteLine("Running host...");
                host.Run();
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Server terminated unexpectedly: {e.Message}");
                Console.WriteLine(e.StackTrace);
                Serilog.Log.Fatal(e, "Server terminated unexpectedly");
                return 1;
            }
            finally
            {
                Serilog.Log.CloseAndFlush();
            }
        }
    }
}
