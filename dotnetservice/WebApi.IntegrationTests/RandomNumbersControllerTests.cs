using System.Text.Json;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using WebApi.Apis.GolangServiceApi;
using WebApi.Configuration;
using WebApi.Controllers;
using WebApi.Data;
using WebApi.DataStructures;
namespace WebApi.IntegrationTests;

public class RandomNumbersControllerTests : BaseIntegrationTest
{
    private readonly RandomNumberController _controllerSut;
    private readonly IGolangService _golangServiceMock;
    private readonly int _numberOfRecordsToReturn;

    public RandomNumbersControllerTests() : base(new IntegrationTestsWebApiFactory())
    {
        _numberOfRecordsToReturn = ServiceProvider.GetRequiredService<IOptions<RandomNumbersControllerConfig>>().Value.RecordsToReturn;
        _golangServiceMock = Substitute.For<IGolangService>();
        _controllerSut = new RandomNumberController(
            Substitute.For<ILogger<RandomNumberController>>(),
            _golangServiceMock,
            ServiceProvider.GetRequiredService<INumbersRepository>(),
            ServiceProvider.GetRequiredService<IOptions<RandomNumbersControllerConfig>>()
            );
    }
    
    [Fact]
    public void GetRandomNumber_ValidRequest_ResponseShouldBeValid()
    {
        const int expectedNumber = 42;
        _golangServiceMock.GetRandomNumber(Arg.Any<GetRandomNumberRequestDto>()).Returns(new GetRandomNumberResponseDto()
        {
            Number = expectedNumber,
        });

        var response = _controllerSut.Get().Result;
        var okObjectResult = response as OkObjectResult;
        okObjectResult.Should().NotBeNull();
        okObjectResult?.StatusCode.Should().Be(200);
        
        var responseDto = JsonSerializer.Deserialize<RandomNumberResponseDto>(okObjectResult?.Value?.ToString() ?? string.Empty);
        
        responseDto.Should().NotBeNull();
        responseDto?.Number.Should().Be(expectedNumber);
        
        responseDto?.Records.Should().NotBeNull();
        responseDto?.Records.Count.Should().BeLessThanOrEqualTo(_numberOfRecordsToReturn);
    }
}