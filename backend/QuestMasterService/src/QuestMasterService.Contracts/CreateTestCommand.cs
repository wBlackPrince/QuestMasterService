namespace QuestMasterService.Core.Features.Tests.CreateTest;

public sealed record CreateTestCommand(
    string Title,
    string Description,
    int MaxAttempts);