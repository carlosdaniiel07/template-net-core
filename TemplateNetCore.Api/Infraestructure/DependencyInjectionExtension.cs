using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            builder.Services.ConfigureBaseServices();

            AddAuthenticationJwt(builder.Services, builder.Configuration);
            
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers(config =>
            {
                config.Filters.Add<NotificationFilter>();
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
