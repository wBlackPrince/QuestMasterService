using CSharpFunctionalExtensions;

namespace QuestMasterService.Domain;

public sealed class QuestionOption
{
    public Guid Id { get; }
    public OptionText Text { get; private set; } = default!;
    public bool IsCorrect { get; private set; }
    public Guid QuestionId { get; }

    // EF Core
    private QuestionOption() { }

    private QuestionOption(Guid id, OptionText text, bool isCorrect, Guid questionId)
    {
        Id = id;
        Text = text;
        IsCorrect = isCorrect;
        QuestionId = questionId;
    }

    public static Result<QuestionOption, AppError> Create(string text, bool isCorrect, Guid questionId)
    {
        var textResult = OptionText.Create(text);
        if (textResult.IsFailure)
            return Result.Failure<QuestionOption, AppError>(textResult.Error);

        return Result.Success<QuestionOption, AppError>(
            new QuestionOption(Guid.NewGuid(), textResult.Value, isCorrect, questionId));
    }

    public UnitResult<AppError> ChangeText(string newText)
    {
        var result = OptionText.Create(newText);
        if (result.IsFailure) return UnitResult.Failure<AppError>(result.Error);
        Text = result.Value;
        return UnitResult.Success<AppError>();
    }

    public UnitResult<AppError> SetCorrectness(bool isCorrect)
    {
        IsCorrect = isCorrect;
        return UnitResult.Success<AppError>();
    }
}