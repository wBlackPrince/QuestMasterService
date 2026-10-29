using CSharpFunctionalExtensions;

namespace QuestMasterService.Domain;

public record TestTitle
{
    public string Value { get; private set; } = string.Empty;

    private TestTitle() { Value = string.Empty; }

    public static Result<TestTitle, AppError> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<TestTitle, AppError>(new AppError("Test title cannot be empty."));
        if (value.Length > 200)
            return Result.Failure<TestTitle, AppError>(new AppError("Test title cannot exceed 200 characters."));

        return Result.Success<TestTitle, AppError>(new TestTitle { Value = value });
    }
}