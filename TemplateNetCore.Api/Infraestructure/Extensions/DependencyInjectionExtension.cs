using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using TemplateNetCore.Domain.Interfaces.Transactions;
using TemplateNetCore.Service.Transactions;
using TemplateNetCore.Repository;
using TemplateNetCore.Repository.Interfaces;
using TemplateNetCore.Repository.EF;
using TemplateNetCore.Repository.EF.Repositories;

namespace TemplateNetCore.Api.Infraestructure.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
        }

        public static IServiceCollection AddScopedServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnityOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddTransientServices(this IServiceCollection services)
        {
            services.AddTransient<ITransactionService, TransactionService>();

            return services;
        }
    }
}
