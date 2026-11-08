using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using QuestMasterService.Infrastructure.Postgres;
using QuestMasterService.Web;
using DotNetEnv;
using System.Globalization;
using QuestMasterService.Core.Abstractions.Repositories;
using QuestMasterService.Infrastructure.Postgres.Repositories;
using QuestMasterService.Core.Features.Tests.CreateTest;
using QuestMasterService.Core.Features.Tests.DeleteTest;
using QuestMasterService.Core.Features.Tests.UpdateTest;
using QuestMasterService.Core.Features.Tests.GetTestById;
using QuestMasterService.Core.Features.Tests.GetTests;
using QuestMasterService.Core.Features.Questions.CreateQuestion;
using QuestMasterService.Core.Features.Questions.UpdateQuestion;
using QuestMasterService.Core.Features.Questions.DeleteQuestion;
using QuestMasterService.Core.Features.Questions.GetQuestionById;
using QuestMasterService.Core.Features.Questions.GetQuestionsByTest;
using QuestMasterService.Core.Questions.Commands.CreateOptionQuestion;
using QuestMasterService.Core.Questions.Commands.CreateTextQuestion;
using QuestMasterService.Core.Questions.Commands.CreateMatchQuestion;

string envPath = Path.GetFullPath(
    Path.Combine(
        Directory.GetCurrentDirectory(),
        "../../.env"));

Env.Load(envPath);

Console.WriteLine($"Current directory: {Directory.GetCurrentDirectory()}");

Console.WriteLine(
    $"Env file exists: {File.Exists(Path.Combine(Directory.GetCurrentDirectory(), ".env"))}");

Console.WriteLine(
    $"Connection string exists: {Environment.GetEnvironmentVariable("CONNECTIONSTRINGS__POSTGRES") is not null}");


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Нативный OpenAPI .NET 9/10
builder.Services.AddOpenApi();

// Для тестового контроллера
builder.Services.AddControllers();

builder.Services.AddScoped<CreateTestHandler>();
builder.Services.AddScoped<DeleteTestHandler>();
builder.Services.AddScoped<UpdateTestHandler>();
builder.Services.AddScoped<GetTestByIdHandler>();
builder.Services.AddScoped<GetTestsHandler>();

builder.Services.AddScoped<UpdateQuestionHandler>();
builder.Services.AddScoped<DeleteQuestionHandler>();
builder.Services.AddScoped<GetQuestionByIdHandler>();
builder.Services.AddScoped<GetQuestionsByTestHandler>();

builder.Services.AddScoped<CreateOptionQuestionHandler>();
builder.Services.AddScoped<CreateTextQuestionHandler>();
builder.Services.AddScoped<CreateMatchQuestionHandler>();

builder.Services.AddScoped<ITestRepository, TestRepository>();
builder.Services.AddScoped<IMatchRepository, MatchRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionOptionRepository, QuestionOptionRepository>();

string connectionString = builder.Configuration.GetConnectionString("Postgres")
    ?? throw new InvalidOperationException("Connection string 'Postgres' is not configured.");

builder.Services.AddDbContext<QuestMasterDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddHealthChecks()
    .AddDbContextCheck<QuestMasterDbContext>();

WebApplication app = builder.Build();

if (args.Contains("--seed", StringComparer.OrdinalIgnoreCase))
{
    var questionCount = GetPositiveIntArgument(args, "--questions", 3000);
    var batchSize = GetPositiveIntArgument(args, "--batch-size", 250);

    await using var scope = app.Services.CreateAsyncScope();
    var db = scope.ServiceProvider.GetRequiredService<QuestMasterDbContext>();
    await DatabaseSeeder.SeedAsync(db, questionCount, batchSize);
    return;
}

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

static int GetPositiveIntArgument(string[] args, string name, int defaultValue)
{
    var index = Array.FindIndex(args, argument => string.Equals(argument, name, StringComparison.OrdinalIgnoreCase));
    if (index < 0)
        return defaultValue;

    if (index == args.Length - 1 || !int.TryParse(args[index + 1], NumberStyles.Integer, CultureInfo.InvariantCulture, out var value) || value < 1)
        throw new ArgumentException($"'{name}' must be followed by a positive integer.", name);

    return value;
}
