namespace QuestMasterService.Core.Features.Tests.UpdateTest;

public sealed record UpdateTestCommand(
    Guid Id,
    string Title,
    string Description,
    int MaxAttempts);