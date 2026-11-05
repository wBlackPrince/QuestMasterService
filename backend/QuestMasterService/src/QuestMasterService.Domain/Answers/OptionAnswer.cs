using CSharpFunctionalExtensions;

namespace QuestMasterService.Domain;

public sealed class OptionsAnswer : Answer
{
    private readonly List<Guid> _userOptions = new();
    public IReadOnlyList<Guid> UserOptions => _userOptions.AsReadOnly();

    // EF Core
    private OptionsAnswer() { }

    private OptionsAnswer(Guid id, Guid questionId, PointsAwarded pointsAwarded,
        AnswerCorrectness isCorrect, Guid attemptId)
        : base(id, AnswerType.Option, questionId, pointsAwarded, isCorrect, attemptId)
    {
    }

    public static Result<OptionsAnswer, AppError> Create(
        Guid questionId, decimal pointsAwarded, AnswerCorrectness isCorrect, Guid attemptId)
    {
        var pointsResult = PointsAwarded.Create(pointsAwarded);
        if (pointsResult.IsFailure)
            return Result.Failure<OptionsAnswer, AppError>(pointsResult.Error);

        return Result.Success<OptionsAnswer, AppError>(
            new OptionsAnswer(Guid.NewGuid(), questionId, pointsResult.Value, isCorrect, attemptId));
    }

    public UnitResult<AppError> AddUserOption(Guid optionId)
    {
        if (_userOptions.Contains(optionId))
            return UnitResult.Failure<AppError>(new AppError("Option already selected."));

        _userOptions.Add(optionId);
        return UnitResult.Success<AppError>();
    }

    public UnitResult<AppError> RemoveUserOption(Guid optionId)
    {
        if (!_userOptions.Remove(optionId))
            return UnitResult.Failure<AppError>(new AppError("Option not found in user selection."));

        return UnitResult.Success<AppError>();
    }

    public UnitResult<AppError> SetPointsAwarded(decimal points)
    {
        var result = PointsAwarded.Create(points);
        if (result.IsFailure) return UnitResult.Failure<AppError>(result.Error);
        PointsAwarded = result.Value;
        return UnitResult.Success<AppError>();
    }

    public UnitResult<AppError> SetCorrectness(AnswerCorrectness correctness)
    {
        IsCorrect = correctness;
        return UnitResult.Success<AppError>();
    }
}