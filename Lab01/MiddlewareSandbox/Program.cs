using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

int requestCount = 0;
app.Use(async (context, next) =>
{
    requestCount++;
    await next();
    await context.Response.WriteAsync($"The amount of processed requests is {requestCount}.\n");
});

app.Use(async (context, next) =>
{
    if (context.Request.Query.ContainsKey("custom"))
    {
        await context.Response.WriteAsync("You've hit a custom middleware!\n");
    }
    else
    {
        await next();
    }
});

app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Request: {Method} {Path}", context.Request.Method, context.Request.Path);
    await next();
});

const string validApiKey = "12345";
app.Use(async (context, next) =>
{
    if (!context.Request.Headers.TryGetValue("X-API-KEY", out var apiKey) || apiKey != validApiKey)
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        await context.Response.WriteAsync("Forbidden: Invalid or missing API key.\n");
        return;
    }
    await next();
});

app.Run(async (context) =>
{
    await context.Response.WriteAsync("Hello from MiddlewareSandbox!\n");
});

app.Run();
