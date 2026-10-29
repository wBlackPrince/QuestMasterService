using CSharpFunctionalExtensions;

namespace QuestMasterService.Domain;

public record PointsCost
{
    public decimal Value { get; private set; }

    private PointsCost() { Value = 0; }

    public static Result<PointsCost, AppError> Create(decimal value)
    {
        if (value < 0)
            return Result.Failure<PointsCost, AppError>(new AppError("Points cost cannot be negative."));
        if (value > 1000)
            return Result.Failure<PointsCost, AppError>(new AppError("Points cost cannot exceed 1000."));

        return Result.Success<PointsCost, AppError>(new PointsCost { Value = value });
    }
}