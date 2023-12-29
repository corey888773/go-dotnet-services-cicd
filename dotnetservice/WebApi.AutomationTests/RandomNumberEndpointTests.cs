using System.Text.Json;
using WebApi.DataStructures;

namespace WebApi.AutomationTests;

public class RandomNumberEndpointTests : IClassFixture<ConfigurationProvider>
{
    private readonly HttpClient _httpClient = new();

    public RandomNumberEndpointTests(ConfigurationProvider configurationProvider)
    {
        _httpClient.BaseAddress = new Uri(configurationProvider.ServiceUrl);
    }

    [Fact]
    public void GET_RandomNumbers()
    {
        var response = _httpClient.GetAsync("/RandomNumber").Result;
        Assert.True(response.IsSuccessStatusCode);
        
        var content = response.Content.ReadAsStringAsync().Result;
        var randomNumberResponse = JsonSerializer.Deserialize<RandomNumberResponseDto>(content);
        Assert.NotNull(randomNumberResponse);
    }
}