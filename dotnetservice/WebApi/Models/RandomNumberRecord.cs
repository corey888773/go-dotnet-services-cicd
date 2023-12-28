using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApi.Models;

public record RandomNumberRecord
{
    public static RandomNumberRecord Create(int number)
    {
        return new RandomNumberRecord
        {
            Id = Guid.NewGuid(),
            Number = number,
            CreatedAt = DateTime.UtcNow
        };
    }
    public Guid Id { get; init; }
    public int Number { get; init; }
    public DateTime CreatedAt { get; init; }
}