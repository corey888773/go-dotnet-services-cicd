namespace WebApi.DataStructures;

public class RandomNumberResponseDto
{
    public int Number { get; set; }
    public List<Record> Records { get; set; } = new();
    
}

public record Record(Guid Id, int Number, DateTime CreatedAt);