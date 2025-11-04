using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using MongoDB.Driver;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using Serilog;
using Serilog.Exceptions;
using TemplateNetCore.Application.Behaviors;
using TemplateNetCore.Application.Commands.v1.Auth.SignIn;
using TemplateNetCore.Application.Commands.v1.Auth.SignUp;
using TemplateNetCore.Domain.Interfaces.Repositories.MongoDb.v1;
using TemplateNetCore.Domain.Interfaces.Repositories.Sql;
using TemplateNetCore.Domain.Interfaces.Services.v1;
using TemplateNetCore.Domain.Models.v1;
using TemplateNetCore.Domain.Services.v1;
using TemplateNetCore.Infrastructure.Data;
using TemplateNetCore.Infrastructure.Data.MongoDb.Repositories.v1;
using TemplateNetCore.Infrastructure.Data.MongoDb.Shared;
using TemplateNetCore.Infrastructure.Data.Sql.Repositories;
using TemplateNetCore.Infrastructure.Service.Services.v1;

namespace TemplateNetCore.Infrastructure.IoC;

public static class DependencyInjectionExtension
{
    public static void ConfigureBaseServices(this WebApplicationBuilder builder)
    {
        AddMediator(builder.Services);
        AddValidators(builder.Services);
        AddAutoMapper(builder.Services);
        AddConfigurationModels(builder.Services, builder.Configuration);
        AddHttpClient(builder.Services);
        AddServices(builder.Services);
        AddInfrastructureServices(builder.Services);
        AddSqlDataServices(builder.Services, builder.Configuration);
        AddMongoDbDataServices(builder.Services, builder.Configuration);
        AddOpenTelemetry(builder);
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
            .AddResilienceHandler("default", builder =>
            {
                builder.AddTimeout(TimeSpan.FromSeconds(10));
                builder.AddRetry(new RetryStrategyOptions<HttpResponseMessage>
                {
                    MaxRetryAttempts = 3,
                    Delay = TimeSpan.FromSeconds(1),
                    BackoffType = DelayBackoffType.Constant,
                });
                builder.AddCircuitBreaker(new CircuitBreakerStrategyOptions<HttpResponseMessage>
                {
                    SamplingDuration = TimeSpan.FromSeconds(10),
                    FailureRatio = 0.9,
                    MinimumThroughput = 5,
                    BreakDuration = TimeSpan.FromSeconds(30),
                });
            });
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<INotificationContext, NotificationContext>();
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

    private static void AddMongoDbDataServices(IServiceCollection services, IConfiguration configuration)
    {
        MongoDbConfiguration.Configure();

        services.AddSingleton(_ =>
        {
            var connectionString = configuration.GetConnectionString("MongoDb");
            var databaseName = connectionString.Split('/').Last().Split('?').First();
            var mongoClient = new MongoClient(connectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseName);

            return mongoDatabase;
        });

        services.AddSingleton<IProductRepository, ProductRepository>();
    }

    private static void AddOpenTelemetry(WebApplicationBuilder builder)
    {
        var otlpEndpoint = Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT");

        if (string.IsNullOrWhiteSpace(otlpEndpoint))
            return;

        builder.Services
            .AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(serviceName: "TemplateNetCore.Api"))
            .WithTracing(tracing =>
            {
                tracing
                    .SetSampler(new AlwaysOnSampler())
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddOtlpExporter();
            });

        builder.Logging.AddOpenTelemetry(logging =>
        {
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;

            logging.AddOtlpExporter();
        });
    }

    private static void AddSerilogSeq(ConfigureHostBuilder host, IConfiguration configuration)
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
