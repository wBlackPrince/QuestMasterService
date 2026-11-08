namespace QuestMasterService.Core.Features.Questions.CreateQuestion;

public sealed record CreateOptionQuestionCommand(
    Guid TestId,
    string Description,
    decimal PointsCost,
    int PositionIndex,
    IReadOnlyCollection<CreateOptionDto> Options);
