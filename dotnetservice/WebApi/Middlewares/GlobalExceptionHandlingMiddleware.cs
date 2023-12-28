using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
    
    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger) => _logger = logger;
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            
            var pd = new ProblemDetails()
            {
                Type = "Internal server error",
                Title = "Internal server error",
                Detail = "Internal server error",
                Status = context.Response.StatusCode,
            };
            
            await context.Response.WriteAsJsonAsync(pd);
        }
    }
}