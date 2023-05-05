using AutoFixture;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using TemplateNetCore.Tests;
using Xunit;

namespace TemplateNetCore.Api.Tests.Controllers
{
    public abstract class BaseControllerTest : IClassFixture<TestWebApplicationFactory>
    {
        protected readonly TestWebApplicationFactory _factory;
        protected readonly HttpClient _httpClient;
        protected readonly Fixture _fixture;

        protected BaseControllerTest(TestWebApplicationFactory factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
            });
            _fixture = new Fixture();
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
    }
}
