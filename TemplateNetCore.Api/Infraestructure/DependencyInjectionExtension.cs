using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TemplateNetCore.Api.Filters;
using TemplateNetCore.Domain.Models.v1;
using TemplateNetCore.Infrastructure.IoC;

namespace TemplateNetCore.Api.Infraestructure
{
    public static class DependencyInjectionExtension
    {
        public static void Configure(this WebApplicationBuilder builder)
        {
            builder.Services.ConfigureBaseServices(builder.Host, builder.Configuration);

            AddControllers(builder.Services);
            AddSwagger(builder.Services);
            AddAuthenticationJwt(builder.Services, builder.Configuration);
        }

        private static void AddControllers(IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                config.Filters.Add<NotificationFilter>();
            });
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
