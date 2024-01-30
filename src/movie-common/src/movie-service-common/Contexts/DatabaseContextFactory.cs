using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Service.Core.Framework.Hosting;
using System;
using System.IO;

namespace Movie.Common.Contexts
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());


            //load main configuration
            configurationBuilder.AddJsonFile($"appsettings.Common.json", optional: false, reloadOnChange: true);

#if !RELEASE
        var env = EnvironmentName.Debug;
    #if DEVELOPMENT
                env = EnvironmentName.Development;
    #elif SANDBOX
                env = EnvironmentName.Sandbox;
    #elif STAGING
                env = EnvironmentName.Staging;
    #elif REVIEW
                env = EnvironmentName.Review;
    #elif TESTING
                env = EnvironmentName.Testing;
    #endif
            //add environtment patch configuration
            configurationBuilder.AddJsonFile($"appsettings.Common.{env}.json", optional: true, reloadOnChange: true);
#endif

            var configuration = configurationBuilder.Build();

            var mysqlConnectionBuilder = new MySqlConnectionStringBuilder(configuration.GetConnectionString("DefaultConnection"));
#if !DEBUG
            mysqlConnectionBuilder.Password = configuration.GetConnectionString("DbPassword");
#endif
            var dbContextBuilder = new DbContextOptionsBuilder();
            dbContextBuilder.UseMySql(
                    mysqlConnectionBuilder.ConnectionString,
                    new MySqlServerVersion(new Version(8, 0, 23)
            ));

            return new DatabaseContext(dbContextBuilder.Options);
        }
    }
}