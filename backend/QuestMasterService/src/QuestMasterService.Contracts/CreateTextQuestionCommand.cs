namespace QuestMasterService.Core.Features.Questions.CreateQuestion;

public sealed record CreateTextQuestionCommand(
    Guid TestId,
    string Description,
    decimal PointsCost,
    int PositionIndex,
    string RightAnswer);
