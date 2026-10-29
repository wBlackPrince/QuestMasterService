using CSharpFunctionalExtensions;

namespace QuestMasterService.Domain;

public record RightAnswerText
{
    public string Value { get; private set; } = string.Empty;

    private RightAnswerText() { Value = string.Empty; }

    public static Result<RightAnswerText, AppError> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<RightAnswerText, AppError>(new AppError("Right answer cannot be empty."));
        if (value.Length > 500)
            return Result.Failure<RightAnswerText, AppError>(new AppError("Right answer cannot exceed 500 characters."));

        return Result.Success<RightAnswerText, AppError>(new RightAnswerText { Value = value });
    }
}