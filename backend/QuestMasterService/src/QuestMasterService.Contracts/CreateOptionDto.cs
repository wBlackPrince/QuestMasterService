namespace QuestMasterService.Core.Features.Questions.CreateQuestion;

public sealed record CreateOptionDto(
    string Text,
    bool IsCorrect);
