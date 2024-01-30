using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using Service.Core.Interfaces.Framework;
using Service.Core.MySql;
using Service.Core.MySql.Models;
using Movie.Common.Contexts;
using Movie.Common.Repositories.MovieRepository;
using Movie.Common.Services.Movie;
using System;
using Movie.Common.Repositories.GenreRepository;
using Movie.Common.Repositories.UserRepository;
using Movie.Common.Repositories.UserReviewRepository;
using Sentry;
using Movie.Common.Services.Genre;
using Movie.Common.Services.User;
using Movie.Common.Services.UserReview;

namespace Movie.Common
{
    public class StartupCommon
    {

        public static void ConfigureServices(IServiceCollection services, MySqlAppSettings mySqlAppSettings)
        {
            ConfigureServices(services, mySqlAppSettings.DefaultConnection, mySqlAppSettings.DbPassword);
        }

        public static void ConfigureServices(IServiceCollection services, string dbAddress, string dbPassword)
        {
            StartupMysql.ConfigureServices(services);
            SetupMySqlDB(services, dbAddress, dbPassword);
            ConfigureServicesSingleton(services);
            ConfigureServicesScopes(services);
        }

        private static void SetupMySqlDB(IServiceCollection services, string dbAddress, string dbPassword)
        {
            var builder = new MySqlConnectionStringBuilder(dbAddress);
            builder.Password = dbPassword;

            services.AddDbContext<DatabaseContext>(options => options.UseMySql(
                    builder.ConnectionString,
                    new MySqlServerVersion(new Version(8, 0, 23)),
                    providerOptions => providerOptions.EnableRetryOnFailure()
            ));
            services.AddScoped<BaseDbContext, DatabaseContext>();
        }

        private static void ConfigureServicesSingleton(IServiceCollection services)
        {
            services.AddSingleton<IMovieRepository, MovieRepository>();
            services.AddSingleton<IGenreRepository, GenreRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IUserReviewRepository, UserReviewRepository>();
        }
        private static void ConfigureServicesScopes(IServiceCollection services)
        {
            //common services
            services.AddScoped<IServerPreRun, DatabaseInitializer>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserReviewService, UserReviewService>();
        }
    }
}
