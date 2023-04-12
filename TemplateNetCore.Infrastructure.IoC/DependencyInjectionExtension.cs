using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TemplateNetCore.Application.Services.v1;
using TemplateNetCore.Application.UseCases.v1.Auth.SignIn;
using TemplateNetCore.Domain.Interfaces.Repositories;
using TemplateNetCore.Domain.Interfaces.Services.v1;
using TemplateNetCore.Domain.UseCases.v1.Auth.SignIn;
using TemplateNetCore.Infrastructure.Data;
using TemplateNetCore.Infrastructure.Service.Services.v1;

namespace TemplateNetCore.Infrastructure.IoC
{
    public static class DependencyInjectionExtension
    {
        public static void ConfigureBaseServices(this IServiceCollection services)
        {
            AddDataServices(services);
            AddInfrastructureServices(services);
            AddApplicationServices(services);
            AddApplicationUseCases(services);
        }

        private static void AddDataServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("TemplateNetCore"));
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnityOfWork, UnityOfWork>();
        }

        private static void AddInfrastructureServices(IServiceCollection services)
        {
            services.AddTransient<IHashService, BCryptHashService>();
            services.AddTransient<ITokenService, JwtTokenService>();
        }

        private static void AddApplicationServices(IServiceCollection services)
        {
            services.AddScoped<INotificationContextService, NotificationContextService>();
        }

        private static void AddApplicationUseCases(IServiceCollection services)
        {
            services.AddScoped<ISignInUseCase, SignInUseCase>();
        }
    }
}
