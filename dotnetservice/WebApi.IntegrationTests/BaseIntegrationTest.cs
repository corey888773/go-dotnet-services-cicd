using Microsoft.Extensions.DependencyInjection;
using WebApi.Data;

namespace WebApi.IntegrationTests;

public abstract class  BaseIntegrationTest : IClassFixture<IntegrationTestsWebApiFactory>
{
    protected readonly IServiceProvider ServiceProvider;
    
    protected BaseIntegrationTest(IntegrationTestsWebApiFactory factory)
    {
        var scope = factory.Services.CreateScope();
        ServiceProvider = scope.ServiceProvider;
    }
}