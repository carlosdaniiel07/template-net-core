using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Polly;
using Serilog;
using Serilog.Exceptions;
using TemplateNetCore.Application.Behaviors;
using TemplateNetCore.Application.Commands.v1.Auth.SignIn;
using TemplateNetCore.Application.Commands.v1.Auth.SignUp;
using TemplateNetCore.Domain.Interfaces.Repositories.MongoDb.v1;
using TemplateNetCore.Domain.Interfaces.Repositories.Sql;
using TemplateNetCore.Domain.Interfaces.Services.v1;
using TemplateNetCore.Domain.Models.v1;
using TemplateNetCore.Infrastructure.Data;
using TemplateNetCore.Infrastructure.Data.MongoDb.Repositories.v1;
using TemplateNetCore.Infrastructure.Data.Sql.Repositories;
using TemplateNetCore.Infrastructure.Service.Services.v1;

namespace TemplateNetCore.Infrastructure.IoC
{
    public static class DependencyInjectionExtension
    {
        public static void ConfigureBaseServices(this IServiceCollection services, ConfigureHostBuilder host, IConfiguration configuration)
        {
            AddMediator(services);
            AddValidators(services);
            AddAutoMapper(services);
            AddConfigurationModels(services, configuration);
            AddHttpClient(services);
            AddInfrastructureServices(services);
            AddSqlDataServices(services, configuration);
            AddMongoDbDataServices(services);
            AddSerilogSeq(host, configuration);
        }

        private static void AddMediator(IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(SignInCommandHandler).Assembly);
                cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
            });
        }

        private static void AddValidators(IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<SignInCommandValidator>();
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(SignUpCommandProfile).Assembly);
        }

        private static void AddConfigurationModels(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(options => configuration.GetSection("JwtSettings").Bind(options));
        }

        private static void AddHttpClient(IServiceCollection services)
        {
            services
                .AddHttpClient<IHttpService, HttpService>()
                .AddPolicyHandler(_ => Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10)))
                .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(1)))
                .AddTransientHttpErrorPolicy(builder => builder.CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 3,
                    durationOfBreak: TimeSpan.FromSeconds(30)
                ));
        }

        private static void AddInfrastructureServices(IServiceCollection services)
        {
            services.AddTransient<IHashService, BCryptHashService>();
            services.AddTransient<ITokenService, JwtTokenService>();
            services.AddSingleton<ICacheService, RedisCacheService>();
        }

        private static void AddSqlDataServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServer")));
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnityOfWork, UnityOfWork>();
        }

        private static void AddMongoDbDataServices(IServiceCollection services)
        {
            services.AddSingleton<IProductRepository, ProductRepository>();
        }

        public static void AddSerilogSeq(ConfigureHostBuilder host, IConfiguration configuration)
        {
            host.UseSerilog((context, config) =>
            {
                config
                    .ReadFrom.Configuration(context.Configuration)
                    .WriteTo.Console()
                    .Enrich.WithProperty("ApplicationName", context.HostingEnvironment.ApplicationName)
                    .Enrich.FromLogContext()
                    .Enrich.WithExceptionDetails()
                    .Enrich.WithEnvironmentName()
                    .Enrich.WithMachineName();

                var seqServerUrl = configuration.GetConnectionString("SeqServerUrl");

                if (!string.IsNullOrWhiteSpace(seqServerUrl))
                    config.WriteTo.Seq(seqServerUrl);
            });
        }

        private static void AddApplicationInsights(IServiceCollection services, IConfiguration configuration)
        {
            var appplicationInsightsConnectionSring = configuration.GetConnectionString("ApplicationInsights");

            if (string.IsNullOrWhiteSpace(appplicationInsightsConnectionSring))
                return;

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
    }
}
