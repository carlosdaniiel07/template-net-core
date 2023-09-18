using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using TemplateNetCore.Domain.Commands.v1.Auth.SignIn;
using TemplateNetCore.Domain.Entities.v1;
using TemplateNetCore.Domain.Interfaces.Services.v1;
using TemplateNetCore.Tests;
using Xunit;

namespace TemplateNetCore.Api.Tests.Controllers.v1.Auth
{
    [Trait("Sut", "SignInController")]
    public class SignInControllerTest : BaseControllerTest
    {
        public SignInControllerTest(TestWebApplicationFactory factory) : base(factory)
        {
            
        }

        [Fact(DisplayName = "Should returns 200 OK on success")]
        public async Task ShouldReturns200OkOnSuccess()
        {
            var hashService = _serviceScope.ServiceProvider.GetRequiredService<IHashService>();
            var request = _fixture.Create<SignInCommand>();
            var user = new User
            {
                Name = _fixture.Create<string>(),
                Email = request.Email,
                Password = hashService.Hash(request.Password),
            };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var response = await _httpClient.PostAsJsonAsync("/api/v1/auth/sign-in", request);
            var result = await GetResultFromHttpResponseAsync<SignInCommandResponse>(response);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Should().NotBeNull();
            result.AccessToken.Should().NotBeNullOrWhiteSpace();
            result.RefreshToken.Should().NotBeNullOrWhiteSpace();
        }

        [Fact(DisplayName = "Should returns 400 Bad Request")]
        public async Task ShouldReturns400BadRequest()
        {
            var request = _fixture.Create<SignInCommand>();
            var response = await _httpClient.PostAsJsonAsync("/api/v1/auth/sign-in", request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
