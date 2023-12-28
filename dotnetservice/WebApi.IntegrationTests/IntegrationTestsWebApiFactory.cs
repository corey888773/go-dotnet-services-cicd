using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using WebApi.Data;

namespace WebApi.IntegrationTests;

public class IntegrationTestsWebApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgresSqlContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("elympics")
        .WithUsername("root")
        .WithPassword("secret")
        .WithPortBinding(5432, 5432)
        .Build();
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<ElympicsDbContext>));
            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }
            services.AddDbContext<ElympicsDbContext>(options =>
            {
                var connectionString = _postgresSqlContainer.GetConnectionString();
                options.UseNpgsql(connectionString);
            });
        });
    }

    public async Task InitializeAsync()
    {
        await _postgresSqlContainer.StartAsync();
    }

    public new Task DisposeAsync()
    {
        return _postgresSqlContainer.StopAsync();
    }
}