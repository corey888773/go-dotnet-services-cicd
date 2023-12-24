namespace WebApi.Models;

public class RandomNumberRecord
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public DateTime CreatedAt { get; set; }
}