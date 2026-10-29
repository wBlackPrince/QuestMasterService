using CSharpFunctionalExtensions;

namespace QuestMasterService.Domain;

public record TotalScore
{
    public decimal Value { get; private set; }

    private TotalScore() { Value = 0; }

    public static Result<TotalScore, AppError> Create(decimal value)
    {
        if (value < 0)
            return Result.Failure<TotalScore, AppError>(new AppError("Total score cannot be negative."));

        return Result.Success<TotalScore, AppError>(new TotalScore { Value = value });
    }
}