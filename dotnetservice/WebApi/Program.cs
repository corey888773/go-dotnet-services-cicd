using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApi.Apis.GolangServiceApi;
using WebApi.Configuration;
using WebApi.Data;
using WebApi.Middlewares;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<GolangServiceConfig>(builder.Configuration.GetSection(nameof(GolangServiceConfig)));
builder.Services.Configure<RandomNumbersControllerConfig>(builder.Configuration.GetSection(nameof(RandomNumbersControllerConfig)));
builder.Services.AddDbContext<ElympicsDbContext>();
builder.Services.AddScoped<INumbersRepository, BasicNumbersRepository>();

builder.Services.AddHttpClient<IGolangService, GolangService>((serviceProvider, client) =>
{
    var golangServiceConfig =
        serviceProvider.GetRequiredService<IOptions<GolangServiceConfig>>().Value;
    client.BaseAddress = new Uri(golangServiceConfig.Url);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.Add("User-Agent", "dotnetservice");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // Run migrations
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ElympicsDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();

namespace WebApi
{
    public partial class Program {}
}