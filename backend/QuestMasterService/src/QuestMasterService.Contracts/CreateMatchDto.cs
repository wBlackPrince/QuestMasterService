namespace QuestMasterService.Core.Features.Questions.CreateQuestion;

public sealed record CreateMatchDto(
    string Left,
    string Right,
    bool IsCorrect);