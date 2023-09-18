using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using TemplateNetCore.Infrastructure.Data;
using Testcontainers.MsSql;
using Xunit;

namespace TemplateNetCore.Tests
{
    public class TestWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder().Build();

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment("Development");
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));
                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(_msSqlContainer.GetConnectionString()));
            });

            return base.CreateHost(builder);
        }

        public Task InitializeAsync() =>
            _msSqlContainer.StartAsync();

        public new Task DisposeAsync() =>
            _msSqlContainer.StopAsync();
    }
}
