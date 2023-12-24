using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.DataStructures.GolangService;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RandomNumberController: ControllerBase
{
    private readonly ILogger<RandomNumberController> _logger;
    private readonly GolangService _golangService;
    private readonly IRepository<RandomNumberRecord> _repository;
    
    public RandomNumberController(ILogger<RandomNumberController> logger, GolangService golangService, IRepository<RandomNumberRecord> repository)
    {
        _logger = logger;
        _golangService = golangService;
        _repository = repository;
    }
    
    [HttpGet(Name = "GetRandomNumber")]
    public async Task<string> Get()
    {
        var request = new GetRandomNumberRequestDto()
        {
            Min = 1,
            Max = 100,
        };
        
        var resp = await _golangService.GetRandomNumber(request);
        
        var record = new RandomNumberRecord()
        {
            Id = Guid.NewGuid(),
            Number = resp.Number,
            CreatedAt = DateTime.UtcNow,
        };
        
        await _repository.AddAsync(record);
        var records = await _repository.ListAsync(take: 2);
        
        return records.Select(r => r.Number).Aggregate("", (acc, n) => acc + n + " ");
    }
}