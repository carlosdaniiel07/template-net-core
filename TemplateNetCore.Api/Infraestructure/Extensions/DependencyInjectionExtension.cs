using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TemplateNetCore.Domain.Interfaces.Transactions;
using TemplateNetCore.Domain.Interfaces.Users;
using TemplateNetCore.Repository;
using TemplateNetCore.Repository.Interfaces;
using TemplateNetCore.Repository.EF;
using TemplateNetCore.Repository.EF.Repositories;
using MediatR;
using TemplateNetCore.Domain.Models;
using TemplateNetCore.Application.Services.Transactions;
using TemplateNetCore.Application.Services.Users;

namespace TemplateNetCore.Api.Infraestructure.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
            return services;
        }

        public static IServiceCollection AddScopedServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnityOfWork, UnityOfWork>();

            return services;
        }

        public static IServiceCollection AddTransientServices(this IServiceCollection services)
        {
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IHashService, HashService>();
            services.AddTransient<ITokenService, TokenService>();

            return services;
        }

        public static IServiceCollection AddSingletonServices(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            var handlersAssembly = AppDomain.CurrentDomain.Load("TemplateNetCore.Application");

            services.AddMediatR(handlersAssembly);

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen();
            return services;
        }

        public static IServiceCollection AddAuthenticationJwt(this IServiceCollection services, IConfiguration configuration)
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
                    ValidIssuer = settings.Issuer,
                    ValidAudience = settings.Audience,
                };
            });

            return services;
        }
    }
}
