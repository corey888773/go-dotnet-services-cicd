using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using WebApi.Configuration;
using WebApi.Services;
using Microsoft.Extensions.Options;
using WebApi.Data;
using WebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<GolangServiceConfig>(builder.Configuration.GetSection(nameof(GolangServiceConfig)));
builder.Services.AddDbContext<ElympicsDbContext>();
builder.Services.AddScoped<IRepository<RandomNumberRecord>, BasicNumbersRepository>();

builder.Services.AddHttpClient<GolangService>((serviceProvider, client) =>
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
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Run migrations
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ElympicsDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();

public partial class Program {}