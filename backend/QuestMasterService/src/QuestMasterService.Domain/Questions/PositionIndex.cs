using CSharpFunctionalExtensions;

namespace QuestMasterService.Domain;

public record PositionIndex
{
    public int Value { get; private set; }

    private PositionIndex() { Value = 0; }

    public static Result<PositionIndex, AppError> Create(int value)
    {
        if (value < 0)
            return Result.Failure<PositionIndex, AppError>(new AppError("Position index cannot be negative."));

        return Result.Success<PositionIndex, AppError>(new PositionIndex { Value = value });
    }
}