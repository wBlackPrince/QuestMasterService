using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using QuestMasterService.Infrastructure.Postgres;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Нативный OpenAPI .NET 9/10
builder.Services.AddOpenApi();

// Для тестового контроллера
builder.Services.AddControllers();

string connectionString = builder.Configuration.GetConnectionString("Postgres")
    ?? throw new InvalidOperationException("Connection string 'Postgres' is not configured.");

builder.Services.AddDbContext<QuestMasterDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddHealthChecks()
    .AddDbContextCheck<QuestMasterDbContext>();

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
