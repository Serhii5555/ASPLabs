var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/who", () => "Serhii Beilakh");
app.MapGet("/time", () => DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));

app.Run();