using Microsoft.AspNetCore.Mvc;
using WebApp.DataStructures.GolangService;
using WebApp.Services;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class RandomNumberController(ILogger<RandomNumberController> logger, GolangService golangService)
    : ControllerBase
{
    private readonly ILogger<RandomNumberController> _logger = logger;

    [HttpGet(Name = "GetRandomNumber")]
    public async Task<string> Get()
    {
        var request = new GetRandomNumberRequestDto()
        {
            Min = 1,
            Max = 100,
        };
        return await golangService.GetRandomNumber(request);
    }
}