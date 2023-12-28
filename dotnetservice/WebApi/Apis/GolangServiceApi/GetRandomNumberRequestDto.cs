using System.Text.Json.Serialization;

namespace WebApi.Apis.GolangServiceApi;
public class GetRandomNumberRequestDto
{
    [JsonPropertyName("min")] public required int Min { get; set; }
    [JsonPropertyName("max")] public required int Max { get; set; }
    [JsonPropertyName("seed")] public int Seed { get; set; } = 0;
}