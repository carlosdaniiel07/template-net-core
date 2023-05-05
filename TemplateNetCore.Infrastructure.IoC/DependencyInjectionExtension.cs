using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.Extensions.Logging;
using TemplateNetCore.Application.Services.v1;
using TemplateNetCore.Application.UseCases.v1.Auth.SignIn;
using TemplateNetCore.Application.UseCases.v1.Auth.SignUp;
using TemplateNetCore.Domain.Interfaces.Repositories;
using TemplateNetCore.Domain.Interfaces.Services.v1;
using TemplateNetCore.Domain.Models.v1;
using TemplateNetCore.Domain.UseCases.v1.Auth.SignIn;
using TemplateNetCore.Domain.UseCases.v1.Auth.SignUp;
using TemplateNetCore.Infrastructure.Data;
using TemplateNetCore.Infrastructure.Service.Services.v1;

namespace TemplateNetCore.Infrastructure.IoC
{
    public static class DependencyInjectionExtension
    {
        public static void ConfigureBaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            AddApplicationInsights(services, configuration);
            AddDataServices(services, configuration);
            AddInfrastructureServices(services);
            AddApplicationServices(services);
            AddApplicationUseCases(services);
            AddAutoMapper(services);
            AddConfigurationModels(services, configuration);
        }

        private static void AddApplicationInsights(IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationInsightsTelemetry(options =>
            {
                options.ConnectionString = configuration.GetConnectionString("ApplicationInsights");
            });
            services.AddLogging(options =>
            {
                options.AddApplicationInsights();
                options.AddFilter<ApplicationInsightsLoggerProvider>((category, logLevel) =>
                {
                    return category.Contains("TemplateNetCore") && logLevel == LogLevel.Information;
                });
            });
        }

        private static void AddDataServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(configuration.GetConnectionString("Sqlite")));
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnityOfWork, UnityOfWork>();
        }

        private static void AddInfrastructureServices(IServiceCollection services)
        {
            services.AddTransient<IHashService, BCryptHashService>();
            services.AddTransient<ITokenService, JwtTokenService>();
            services.AddSingleton<ICacheService, RedisCacheService>();
        }

        private static void AddApplicationServices(IServiceCollection services)
        {
            services.AddScoped<INotificationContextService, NotificationContextService>();
        }

        private static void AddApplicationUseCases(IServiceCollection services)
        {
            services.AddScoped<ISignInUseCase, SignInUseCase>();
            services.AddScoped<ISignUpUseCase, SignUpUseCase>();
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(SignUpProfile).Assembly);
        }

        private static void AddConfigurationModels(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(options => configuration.GetSection("JwtSettings").Bind(options));
        }
    }
}
