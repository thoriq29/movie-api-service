using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HostFiltering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Service.Core.Framework.Extensions;
using Service.Core.Framework.Program;
using Serilog;
using System.Reflection;
using System.IO;
using System;

namespace Service.Core.Framework.Hosting
{
    public static class WebHost
    {
        public static IConfiguration Configuration { get; private set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="WebHostBuilder" /> class with pre-configured defaults.
        /// </summary>
        /// <remarks>
        ///     The following defaults are applied to the returned <see cref="WebHostBuilder" />:
        ///     use Kestrel as the web server and configure it using the application's configuration providers,
        ///     set the <see cref="IHostingEnvironment.ContentRootPath" /> to the result of
        ///     <see cref="Directory.GetCurrentDirectory()" />,
        ///     load <see cref="IConfiguration" /> from 'appsettings.json' and 'appsettings.[
        ///     <see cref="IHostingEnvironment.EnvironmentName" />].json',
        ///     load <see cref="IConfiguration" /> from User Secrets when <see cref="IHostingEnvironment.EnvironmentName" /> is
        ///     'Development' or 'Debug' using the entry assembly,
        ///     load <see cref="IConfiguration" /> from environment variables,
        ///     load <see cref="IConfiguration" /> from supplied command line args,
        ///     configures the <see cref="ILogger" /> to log to the console and debug output,
        ///     enables IIS integration,
        ///     and enables the ability for frameworks to bind their options to their default configuration sections.
        /// </remarks>
        /// <param name="args">The command line args.</param>
        /// <param name="appConfig">App Config.</param>
        /// <returns>The initialized <see cref="IWebHostBuilder" />.</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args, BackendCoreAppConfig appConfig)
        {
            var builder = new WebHostBuilder()
                .UseKestrel((builderContext, options) =>
                {
                    options.Configure(builderContext.Configuration.GetSection("Kestrel"));
                })
                .CaptureStartupErrors(true)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    ConfigureAppConfiguration(hostingContext, config, args, appConfig);
                }).UseSerilog((ctx, lc) => lc
                    .Enrich.FromLogContext()
                    .ReadFrom.Configuration(ctx.Configuration))
                .ConfigureServices((hostingContext, services) =>
                {
                    // Fallback
                    services.PostConfigure<HostFilteringOptions>(options =>
                    {
                        if (options.AllowedHosts == null || options.AllowedHosts.Count == 0)
                        {
                            // "AllowedHosts": "localhost;127.0.0.1;[::1]"
                            var hosts = hostingContext.Configuration["AllowedHosts"]
                                ?.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            // Fall back to "*" to disable.
                            options.AllowedHosts = hosts?.Length > 0 ? hosts : new[] { "*" };
                        }
                    });
                    // Change notification
                    services.AddSingleton<IOptionsChangeTokenSource<HostFilteringOptions>>(
                        new ConfigurationChangeTokenSource<HostFilteringOptions>(hostingContext.Configuration));

                    services.AddTransient<IStartupFilter, HostFilteringStartupFilter>();
                })
                .UseIISIntegration()
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                })
                .UseSentry();

            if (args != null) builder.UseConfiguration(new ConfigurationBuilder().AddCommandLine(args).Build());

            return builder;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="WebHostBuilder" /> class with pre-configured defaults using typed
        ///     Startup.
        /// </summary>
        /// <remarks>
        ///     The following defaults are applied to the returned <see cref="WebHostBuilder" />:
        ///     use Kestrel as the web server and configure it using the application's configuration providers,
        ///     set the <see cref="IHostingEnvironment.ContentRootPath" /> to the result of
        ///     <see cref="Directory.GetCurrentDirectory()" />,
        ///     load <see cref="IConfiguration" /> from 'appsettings.json' and 'appsettings.[
        ///     <see cref="IHostingEnvironment.EnvironmentName" />].json',
        ///     load <see cref="IConfiguration" /> from User Secrets when <see cref="IHostingEnvironment.EnvironmentName" /> is
        ///     'Development' using the entry assembly,
        ///     load <see cref="IConfiguration" /> from environment variables,
        ///     load <see cref="IConfiguration" /> from supplied command line args,
        ///     configures the <see cref="ILogger" /> to log to the console and debug output,
        ///     enables IIS integration,
        ///     enables the ability for frameworks to bind their options to their default configuration sections.
        ///     Add Dynamic Load App Setting Json File
        /// </remarks>
        /// <typeparam name="TStartup">The type containing the startup methods for the application.</typeparam>
        /// <param name="args">The command line args.</param>
        /// <param name="appConfig">App Config.</param>
        /// <returns>The initialized <see cref="IWebHostBuilder" />.</returns>
        public static IWebHostBuilder CreateWebHostBuilder<TStartup>(string[] args, BackendCoreAppConfig appConfig) where TStartup : class
        {
            return CreateWebHostBuilder(args, appConfig).UseStartup<TStartup>();
        }

        private static void ConfigureAppConfiguration(WebHostBuilderContext hostingContext, IConfigurationBuilder config, string[] args, BackendCoreAppConfig appConfig)
        {
            var env = hostingContext.HostingEnvironment;

            env.EnvironmentName = BackendCoreProgram.environment;

            Console.WriteLine($"Environment Name: {env.EnvironmentName} ");

            Configuration = hostingContext.Configuration;

            string projectDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            appConfig.AppSettingFilePaths.ForEach(filepath =>
            {
                Console.WriteLine($"Appsetting path: {projectDir}/{filepath}.json");

                Console.WriteLine($"Appsetting common path: {projectDir}/{filepath}.Common.json");
                //load common configuration (madatory)
                config.AddJsonFile($"{projectDir}/{filepath}.Common.json", optional: false, reloadOnChange: true);

                if (env.IsNotRelease())
                {
                    Console.WriteLine($"Appsetting environment path: {projectDir}/{filepath}.Common.{env.EnvironmentName}.json");
                    config.AddJsonFile($"{projectDir}/{filepath}.Common.{env.EnvironmentName}.json", optional: false, reloadOnChange: true);
                }

                //load main configuration (optional)
                config.AddJsonFile($"{projectDir}/{filepath}.json", optional: true, reloadOnChange: true);

                if (env.IsNotRelease())
                {
                    Console.WriteLine($"Appsetting environment common path: {projectDir}/{filepath}.{env.EnvironmentName}.json");
                    config.AddJsonFile($"{projectDir}/{filepath}.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                }
            });

            if (env.IsDevelopment() || env.IsDebug())
            {
                var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                if (appAssembly != null) config.AddUserSecrets(appAssembly, true);
            }

            if (env.IsNotDebug())
            {
                config.AddGoogleCloudSecrets();
            }
            config.AddEnvironmentVariables();

            if (args != null) config.AddCommandLine(args);
        }
    }

    internal class HostFilteringStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseHostFiltering();
                next(app);
            };
        }
    }
}
