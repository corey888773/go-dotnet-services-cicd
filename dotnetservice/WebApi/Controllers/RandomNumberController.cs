using System.Data;
using System.Net;
using System.Text.Json;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Apis.GolangServiceApi;
using WebApi.Configuration;
using WebApi.Data;
using WebApi.DataStructures;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RandomNumberController: ControllerBase
{
    private readonly ILogger<RandomNumberController> _logger;
    private readonly IGolangService _golangService;
    private readonly INumbersRepository _repository;
    private readonly int _numberOfRecordsToReturn;
    
    private readonly JsonSerializerOptions _jsonSerializerOptions = new ()
    {
        WriteIndented = true,
    };
    
    public RandomNumberController(
        ILogger<RandomNumberController> logger, 
        IGolangService golangService, 
        INumbersRepository repository,
        IOptions<RandomNumbersControllerConfig> options)
    {
        _logger = logger;
        _golangService = golangService;
        _repository = repository;
        _numberOfRecordsToReturn = options.Value.RecordsToReturn;
    }
    
    [HttpGet(Name = "GetRandomNumber")]
    public async Task<IActionResult> Get()
    {
        var request = new GetRandomNumberRequestDto()
        {
            Min = 1,
            Max = 100,
        };
        
        var resp = await _golangService.GetRandomNumber(request);
        var record = RandomNumberRecord.Create(resp.Number);
        
        await _repository.AddAsync(record);
        var records = await _repository.ListAsync(take: _numberOfRecordsToReturn);

        var response = new RandomNumberResponseDto()
        {
            Number = record.Number,
            Records = records.Select(r => new Record(r.Id, r.Number, r.CreatedAt)).ToList(),
        };  
        
        return Ok(JsonSerializer.Serialize(response, _jsonSerializerOptions));
    }
}