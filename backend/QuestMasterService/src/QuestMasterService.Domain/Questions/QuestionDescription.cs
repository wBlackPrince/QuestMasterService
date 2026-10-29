using CSharpFunctionalExtensions;

namespace QuestMasterService.Domain;

public record QuestionDescription
{
    public string Value { get; private set; } = string.Empty;

    private QuestionDescription() { Value = string.Empty; }

    public static Result<QuestionDescription, AppError> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<QuestionDescription, AppError>(new AppError("Question description cannot be empty."));
        if (value.Length > 2000)
            return Result.Failure<QuestionDescription, AppError>(new AppError("Question description cannot exceed 2000 characters."));

        return Result.Success<QuestionDescription, AppError>(new QuestionDescription { Value = value });
    }
}