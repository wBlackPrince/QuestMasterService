namespace QuestMasterService.Core.Features.Questions.UpdateQuestion;

public sealed record UpdateQuestionCommand(
    Guid Id,
    string Description,
    decimal PointsCost,
    int PositionIndex);