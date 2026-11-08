namespace QuestMasterService.Core.Features.Questions.CreateQuestion;

public sealed record CreateMatchQuestionCommand(
    Guid TestId,
    string Description,
    decimal PointsCost,
    int PositionIndex,
    IReadOnlyCollection<CreateMatchDto> Matches);
