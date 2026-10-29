using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Нативный OpenAPI .NET 9/10
builder.Services.AddOpenApi();

// Для тестового контроллера
builder.Services.AddControllers();

builder.Services.AddHealthChecks();

WebApplication app = builder.Build();

// Minimal API endpoints
app.MapGet("/", () => "QuestMasterService is running!");

// MVC контроллеры
app.MapControllers();

// Health checks
app.MapHealthChecks("/health");

if (!app.Environment.IsProduction())
{
    app.MapOpenApi();              // /openapi/v1.json
    app.MapScalarApiReference();   // /scalar/v1
}

await app.RunAsync();