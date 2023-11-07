﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.RateLimiting;
using TemplateNetCore.Domain.Models.v1;
using TemplateNetCore.Infrastructure.IoC;

namespace TemplateNetCore.Api.Infraestructure
{
    public static class DependencyInjectionExtension
    {
        public static void Configure(this WebApplicationBuilder builder)
        {
            builder.Services.ConfigureBaseServices(builder.Host, builder.Configuration);

            AddRateLimit(builder.Services);
            AddControllers(builder.Services);
            AddSwagger(builder.Services);
            AddAuthenticationJwt(builder.Services, builder.Configuration);
        }

        private static void AddRateLimit(IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                options.AddPolicy("fixed", httpContext =>
                {
                    return RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 10,
                            Window = TimeSpan.FromSeconds(10),
                        }
                    );
                });
            });
        }

        private static void AddControllers(IServiceCollection services)
        {
            services.AddControllers();
        }

        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.TagActionsBy(api =>
                {
                    if (api.GroupName != null)
                        return new string[] { api.GroupName };

                    return new string[] { (api.ActionDescriptor as ControllerActionDescriptor).ControllerName };
                });

                config.DocInclusionPredicate((name, api) => true);
            });
        }

        private static void AddAuthenticationJwt(IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.Secret)),
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidIssuer = settings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = settings.Audience,
                };
            });
        }
    }
}
