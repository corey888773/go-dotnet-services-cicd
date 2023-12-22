using System.Net.Http.Headers;
using WebApi.Configuration;
using WebApi.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<GolangServiceConfig>(builder.Configuration.GetSection(nameof(GolangServiceConfig)));

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

app.Run();
