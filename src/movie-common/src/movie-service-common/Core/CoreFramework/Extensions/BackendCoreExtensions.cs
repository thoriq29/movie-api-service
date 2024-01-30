using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Service.Core.Framework.Program;
using Service.Core.Framework.Services;
using Service.Core.Framework.Utils;
using Service.Core.Interfaces.Framework;
using Service.Core.Interfaces.Log;
using Newtonsoft.Json.Converters;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Movie.Common.Models.User;
using Microsoft.AspNetCore.Identity;
using Movie.Common.Contexts;

namespace Service.Core.Framework.Extensions
{
    public static class BackendCoreExtensions
    {
        public static IServiceCollection AddBackendCoreServiceConfiguration(this IServiceCollection services, string authorityUrl, IErrorFactory errorFactory, IRedisServerOptions redisServerOptions, ISwaggerInfo swaggerInfo)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IServiceWrapper, ServiceWrapper>();

            // Register MVC:
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            }).AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()))
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    return new InvalidModelStateError(errorFactory).CustomErrorResponse(actionContext);
                };
            });

            // configure jwt authentication
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.Authority = authorityUrl;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Do not change the builder invocation because it might be sequence dependent
            var MvcCoreBuilder = services.AddMvcCore();
            //MvcCoreBuilder.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            MvcCoreBuilder.AddApiExplorer();
            MvcCoreBuilder.AddAuthorization();

            MvcCoreBuilder.AddFormatterMappings();

            services.Configure<MvcOptions>(opt => { opt.RequireHttpsPermanent = false; });

            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route movies
                    options.SubstituteApiVersionInUrl = true;
                });

            services.AddApiVersioning(o =>
            {
                //CONSIDER: reuse IsNotRelease from IWebHostEnvironment extension
                if (BackendCoreProgram.environment != Hosting.EnvironmentName.Release)
                {
                    o.ReportApiVersions = true;
                }
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
            services
                .AddSwaggerGen(
                    options =>
                    {
                        // add a swagger document for each discovered API version
                        // note: you might choose to skip or document deprecated API versions differently
                        foreach (var description in provider.ApiVersionDescriptions)
                            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(swaggerInfo, description));

                        // add a custom operation filter which sets default values
                        options.OperationFilter<SwaggerDefaultValues>();

                        // integrate xml comments
                        options.IncludeXmlComments(XmlCommentsFilePath);

                        options.AddSecurityDefinition("Bearer",
                        new OpenApiSecurityScheme
                        {
                            In = ParameterLocation.Header,
                            Description = "Please enter into field the word 'Bearer' following by space and JWT",
                            Name = "Authorization",
                            Type = SecuritySchemeType.ApiKey
                        });

                        options.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                            {
                            new OpenApiSecurityScheme
                            {
                            Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new List<string>()
                            }
                        });
                    });

            services.AddSwaggerGenNewtonsoftSupport();

            // Register Redis Cache
            if (redisServerOptions.Enabled)
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = redisServerOptions.CacheConfiguration;
                    options.InstanceName = redisServerOptions.InstanceName;
                });
            }

            services.AddHttpClient();

            services.AddDataProtection().UseCryptographicAlgorithms(
                new AuthenticatedEncryptorConfiguration
                {
                    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                    ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                });

            return services;
        }

        public static IApplicationBuilder UseBackendCoreService(this IApplicationBuilder app, IWebHostEnvironment env,
            IHostApplicationLifetime lifetime, IDistributedCache cache, IApiVersionDescriptionProvider provider, string[] allowedOrigins)
        {
            app.Use(async (context, next) =>
            {
                try
                {
                    string bearer = context.Request.Headers["Authorization"];
                    bearer ??= "";
                    var accessToken = bearer.Replace("Bearer ", "");
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(accessToken);
                    var userIdClaim = jwtToken.Claims.SingleOrDefault(x => x.Type.Equals(JwtRegisteredClaimNames.NameId));
                    context.Items[JwtRegisteredClaimNames.NameId] = userIdClaim is null ? "" : userIdClaim.Value;
                }
                catch
                {
                    //do nothing
                }
                context.Request.EnableBuffering();
                await next.Invoke();
            });


            // sentry tracing for performance monitoring
            app.UseSentryTracing();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            Console.WriteLine("Allowed Origins Domain: " + string.Join(',', allowedOrigins));
            app.UseCors(cb => cb.WithOrigins(allowedOrigins).SetIsOriginAllowedToAllowWildcardSubdomains().AllowAnyHeader().AllowAnyMethod());

            app.UseAuthentication();
            app.UseMvc();

            // Redis
            lifetime.ApplicationStarted.Register(() =>
            {
                var currentTimeUTC = DateTime.UtcNow.ToString();
                var encodedCurrentTimeUTC = Encoding.UTF8.GetBytes(currentTimeUTC);
                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));
                cache.Set("cachedTimeUTC", encodedCurrentTimeUTC, options);
            });

            // Swagger
            if (env.EnvironmentName == "Debug" || env.EnvironmentName == "Development" || env.EnvironmentName == "Staging" || env.EnvironmentName == "Sandbox")
            {
                app.UseSwagger();
                app.UseSwaggerUI(
                    options =>
                    {
                        // build a swagger endpoint for each discovered API version
                        foreach (var description in provider.ApiVersionDescriptions)
                            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                                description.GroupName.ToUpperInvariant());
                    });
            }

            return app;
        }

        private static OpenApiInfo CreateInfoForApiVersion(ISwaggerInfo swaggerInfo, ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = swaggerInfo.Title ?? "Game Server",
                Version = "1.0.0",
                Description = swaggerInfo.Description ?? "Server backend features",
                Contact = new OpenApiContact { Name = "Mythic Alliance Pte Ltd" }

            };

            if (description.IsDeprecated) info.Description += " This API version has been deprecated.";

            return info;
        }

        private static string XmlCommentsFilePath
        {
            get
            {
                var basePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                var fileName = Assembly.GetEntryAssembly().GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }
    }
}
