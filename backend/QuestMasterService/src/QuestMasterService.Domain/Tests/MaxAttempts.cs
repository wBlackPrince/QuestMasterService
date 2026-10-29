using CSharpFunctionalExtensions;

namespace QuestMasterService.Domain;

public record MaxAttempts
{
    public int Value { get; private set; }

    private MaxAttempts() { Value = 0; }

    public static Result<MaxAttempts, AppError> Create(int value)
    {
        if (value < 1)
            return Result.Failure<MaxAttempts, AppError>(new AppError("Max attempts must be at least 1."));
        if (value > 100)
            return Result.Failure<MaxAttempts, AppError>(new AppError("Max attempts cannot exceed 100."));

        return Result.Success<MaxAttempts, AppError>(new MaxAttempts { Value = value });
    }
}