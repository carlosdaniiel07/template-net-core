using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

namespace TemplateNetCore.Infrastructure.IoC;

public static class HttpClientBuilderExtensions
{
    public static IHttpResiliencePipelineBuilder AddDefaultResilienceHandler(this IHttpClientBuilder builder, int timeoutInSeconds = 10, int? maxRetryAttempts = 3, bool useCircuitBreaker = true)
    {
        return builder.AddResilienceHandler("default", builder =>
        {
            builder.AddTimeout(TimeSpan.FromSeconds(timeoutInSeconds));

            if (maxRetryAttempts.HasValue)
            {
                builder.AddRetry(new RetryStrategyOptions<HttpResponseMessage>
                {
                    MaxRetryAttempts = maxRetryAttempts.Value,
                    Delay = TimeSpan.FromSeconds(1),
                    BackoffType = DelayBackoffType.Constant,
                });
            }

            if (useCircuitBreaker)
            {
                builder.AddCircuitBreaker(new CircuitBreakerStrategyOptions<HttpResponseMessage>
                {
                    SamplingDuration = TimeSpan.FromSeconds(10),
                    FailureRatio = 0.9,
                    MinimumThroughput = 5,
                    BreakDuration = TimeSpan.FromSeconds(30),
                });
            }
        });
    }
}