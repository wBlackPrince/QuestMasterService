using CSharpFunctionalExtensions;

namespace QuestMasterService.Domain;

public record TestDescription
{
    public string Value { get; private set; } = string.Empty;

    private TestDescription() { Value = string.Empty; }

    public static Result<TestDescription, AppError> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<TestDescription, AppError>(new AppError("Test description cannot be empty."));
        if (value.Length > 2000)
            return Result.Failure<TestDescription, AppError>(new AppError("Test description cannot exceed 2000 characters."));

        return Result.Success<TestDescription, AppError>(new TestDescription { Value = value });
    }
}