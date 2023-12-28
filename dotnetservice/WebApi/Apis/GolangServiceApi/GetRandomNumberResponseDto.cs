using System.Text.Json.Serialization;

namespace WebApi.Apis.GolangServiceApi;

public class GetRandomNumberResponseDto
{
    [JsonPropertyName("number")]
    public int Number { get; set; }

    [JsonPropertyName("seed")] public int Seed { get; set; }
}