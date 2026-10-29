using CSharpFunctionalExtensions;

namespace QuestMasterService.Domain;

public record PointsAwarded
{
    public decimal Value { get; private set; }

    private PointsAwarded() { Value = 0; }

    public static Result<PointsAwarded, AppError> Create(decimal value)
    {
        if (value < 0)
            return Result.Failure<PointsAwarded, AppError>(new AppError("Points awarded cannot be negative."));
        if (value > 1000)
            return Result.Failure<PointsAwarded, AppError>(new AppError("Points awarded cannot exceed 1000."));

        return Result.Success<PointsAwarded, AppError>(new PointsAwarded { Value = value });
    }
}