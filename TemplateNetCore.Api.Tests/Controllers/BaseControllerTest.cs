using AutoFixture;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using TemplateNetCore.Infrastructure.Data;
using TemplateNetCore.Tests;
using Xunit;

namespace TemplateNetCore.Api.Tests.Controllers;

public abstract class BaseControllerTest : IClassFixture<TestWebApplicationFactory>, IDisposable
{
    protected readonly TestWebApplicationFactory _factory;
    protected readonly IServiceScope _serviceScope;
    protected readonly HttpClient _httpClient;
    protected readonly ApplicationDbContext _dbContext;
    protected readonly Fixture _fixture;

    protected BaseControllerTest(TestWebApplicationFactory factory)
    {
        _factory = factory;
        _serviceScope = factory.Services.CreateScope();
        _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false,
        });
        _dbContext = _serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        _fixture = new Fixture();

        _dbContext.Database.Migrate();
    }

    protected async Task<TResponse> GetResultFromHttpResponseAsync<TResponse>(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
            return default;

        var json = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<TResponse>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        _serviceScope?.Dispose();
        _dbContext?.Dispose();
    }
}
