using System.Text.Json.Serialization;

namespace WebApi.DataStructures.GolangService;
public class GetRandomNumberRequestDto
{
    [JsonPropertyName("min")]
    public int Min { get; set; }
    [JsonPropertyName("max")]
    public int Max { get; set; }
    [JsonPropertyName("seed")]
    public int Seed { get; set; } = 0;
}