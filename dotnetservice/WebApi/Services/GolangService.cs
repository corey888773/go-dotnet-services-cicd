using System.Text;
using System.Text.Json;
using WebApi.DataStructures.GolangService;

namespace WebApi.Services;

public class GolangService
{
    private readonly HttpClient _client;
    private readonly Uri _uri;

    public GolangService(HttpClient client)
    {
        _client = client;
        _uri = _client.BaseAddress ?? throw new Exception(nameof(client.BaseAddress));
    }

    public async Task<string> GetRandomNumber(GetRandomNumberRequestDto requestDto)
    {
        var httpRequest = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(_uri, "random"),
            Content = new StringContent(JsonSerializer.Serialize(requestDto), Encoding.UTF8, "application/json")
        };
        
        var response = await _client.SendAsync(httpRequest);
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        return content;
    }
}