using Microsoft.Extensions.Configuration;

namespace WebApi.AutomationTests;

public class ConfigurationProvider
{
    public required IConfiguration Configuration { get; set; }
    public readonly string ServiceUrl;

    public ConfigurationProvider()
    {
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("automationSettings.json", false, true);
            
        Configuration = configurationBuilder.Build();
        ServiceUrl = GetServiceUrl();
    }
    
    private string GetServiceUrl()
    {
        var serviceUrl = Environment.GetEnvironmentVariable("ServiceUrl");
        if (string.IsNullOrEmpty(serviceUrl))
        {
            serviceUrl = Configuration["ServiceUrl"];
        }
        if (string.IsNullOrEmpty(serviceUrl))
        {
            throw new ArgumentException("ServiceUrl is not set");
        }

        return serviceUrl;
    }
}