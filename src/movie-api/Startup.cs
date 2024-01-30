using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Service.Core.Framework;
using Service.Core.Framework.Configurations;
using Service.Core.Framework.Extensions;
using Service.Core.Framework.Services;
using Service.Core.Interfaces.Caching;
using Service.Core.Interfaces.Framework;
using Serilog;
using System;
using System.Linq;
using Movie.Api.Constants;
using Movie.Api.Dto;
using Service.Core.Interfaces.Log;
using Movie.Common;
using Service.Core.Framework.Movies;
using Service.Core.Log;
using Service.Core.Log.Utils;
using Service.Core.Log.Logger;
using Service.Core.Log.Errors;
using Service.Core.MySql.Models;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Service.Utilities.AuthorizationPolicies;
using Service.Movie.Common.Constants;
using Movie.Api.Dto.Genre;
using Movie.Api.Services.Command.Genre;
using Movie.Api.Services.Command;
using Movie.Api.Services.Command.Movie;
using Movie.Api.Dto.Movie;
using Movie.Api.Services.Command.UserReview;
using Movie.Api.Services.Command.User;
using Movie.Api.Dto.UserReview;
using Movie.Api.Services.Account;
using Service.ServerToServer.Service.MythicAccountService;
using Service.ServerToServer.Service.ServerRequestService;
using Movie.Api.Services.Query.Genre;
using Movie.Api.Services.Query.Movie;
using Movie.Api.Services.Query.UserReview;
using Movie.Api.Services.Caching;

namespace Movie.Api
{
    public sealed class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IErrorFactory errorFactory;
        private readonly ICoreLogger coreLogger;

        public Startup(IConfiguration configuration, ILogger logger)
        {
            this.configuration = configuration;
            this.errorFactory = new ErrorFactory();
            this.coreLogger = new CoreLogger(logger);
            // MethodTimeLogger.coreLogger = this.coreLogger;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime, IDistributedCache cache, IApiVersionDescriptionProvider provider)
        {
            var allowedDomain = AllowedOriginsConstant.DOMAIN;
            if (env.EnvironmentName == "Debug" || env.EnvironmentName == "Development" || env.EnvironmentName == "Staging")
            {
                allowedDomain = allowedDomain.Concat(AllowedOriginsConstant.DOMAIN_LOCAL_DEV).ToArray();
            }
            app.UseBackendCoreService(env, lifetime, cache, provider, allowedDomain);

            app.UseAuthentication();
            app.UseAuthorization();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var authorityUrl = configuration.GetConnectionString("Authority");

            SetupAuthority(services, authorityUrl);

            var redisServerOptions = configuration.GetSection("RedisServer").Get<RedisServerOptions>();
            var swaggerInfoOptions = configuration.GetSection("Swagger").Get<SwaggerInfoOption>();

            services.AddBackendCoreServiceConfiguration(authorityUrl, errorFactory, redisServerOptions, swaggerInfoOptions);

            services.AddAutoMapper(typeof(Startup));

            //SetupJWT(services);
            ConfigureServicesSingleton(services);
            ConfigureServicesScopes(services);

            SetupCommon(services);
        }
        private void SetupAuthority(IServiceCollection services, string authorityUrl)
        {
            //TODO: this should be handled by removing the postfix slash instead of manually reducing one length
            var issuer = authorityUrl.Remove(authorityUrl.Length - 1, 1);
            services.AddAuthorization(options =>
            {
                options.AddPolicy("HasAuthorization", policy =>
                    policy.Requirements.Add(new HasAuthorizationRequirement(AuthorizationConstant.SCOPE, issuer, AuthorizationConstant.ROLE)));
            });

            //Alternative Auth:
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("HasAuthorization", policy =>
            //    {
            //        policy.RequireScope(AuthorizationConstant.SCOPE);
            //        policy.RequireRole(AuthorizationConstant.ROLE);
            //        policy.RequireClaim("iss", issuer);
            //    });
            //});

#if DEBUG
            IdentityModelEventSource.ShowPII = true;
#endif
        }

        private void ConfigureServicesSingleton(IServiceCollection services)
        {
            //read the necessary setup comment

            //just manually inject instead
            services.AddSingleton<ICoreLogger>(coreLogger);
            services.TryAddSingleton<IError, Error>();
            services.AddSingleton<IErrorFactory, ErrorFactory>();
            services.AddSingleton<IAuthorizationHandler, HasAuthorizationHandler>();
        }

        private static void ConfigureServicesScopes(IServiceCollection services)
        {
            //CoreLogger, Error and ErrorFactory already declared in Singleton service
            StartupLogger.NecessaryInjection();
            StartupFramework.NecessaryInjection<Error, ErrorFactory>();

            services.AddScoped<IServiceResultTemplate, ServiceResultTemplate>();

            //server to server
            services.AddScoped<IServerRequestService, ServerRequestService>();

            //mythic account
            services.AddScoped<IMythicAccountService, MythicAccountService>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IGenreCommandService, GenreCommandService>();
            services.AddScoped<IMovieCommandService, MovieCommandService>();
            services.AddScoped<IUserReviewCommandService, UserReviewCommandService>();
            services.AddScoped<IUserCommandService, UserCommandService>();

            services.AddScoped<IGenreQueryService, GenreQueryService>();
            services.AddScoped<IMovieQueryService, MovieQueryService>();
            services.AddScoped<IUserReviewQueryService, UserReviewQueryService>();

            //caching service
            services.AddScoped<ICachingService, CachingService>();
            services.AddScoped<IMovieCachingService, MovieCachingService>();

            //fluent validation class
            services.AddScoped<IValidator<GenrePostRequestDto>, GenrePostRequestDtoValidator>();
            services.AddScoped<IValidator<GenrePutRequestDto>, GenrePutRequestDtoValidator>();

            services.AddScoped<IValidator<MoviePostRequestDto>, MoviePostRequestDtoValidator>();
            services.AddScoped<IValidator<MoviePutRequestDto>, MoviePutRequestDtoValidator>();
            services.AddScoped<IValidator<UserReviewRequestDto>, UserReviewRequestDtoValidator>();
        }
       
        private void SetupCommon(IServiceCollection services)
        {
            var mySqlAppSettings = configuration.GetSection("ConnectionStrings").Get<MySqlAppSettings>();
            StartupCommon.ConfigureServices(services, mySqlAppSettings);
        }
    }
}