using CSharpFunctionalExtensions;

namespace QuestMasterService.Domain;

public record UserAnswerText
{
    public string Value { get; private set; } = string.Empty;

    private UserAnswerText() { Value = string.Empty; }

    public static Result<UserAnswerText, AppError> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<UserAnswerText, AppError>(new AppError("User answer cannot be empty."));
        if (value.Length > 500)
            return Result.Failure<UserAnswerText, AppError>(new AppError("User answer cannot exceed 500 characters."));

        return Result.Success<UserAnswerText, AppError>(new UserAnswerText { Value = value });
    }
}