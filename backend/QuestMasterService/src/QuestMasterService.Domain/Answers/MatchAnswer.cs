using CSharpFunctionalExtensions;

namespace QuestMasterService.Domain;

public sealed class MatchAnswer : Answer
{
    private readonly List<Guid> _userMatches = new();
    public IReadOnlyList<Guid> UserMatches => _userMatches.AsReadOnly();

    // EF Core
    private MatchAnswer() { }

    private MatchAnswer(Guid id, Guid questionId, PointsAwarded pointsAwarded,
        AnswerCorrectness isCorrect, Guid attemptId)
        : base(id, AnswerType.Match, questionId, pointsAwarded, isCorrect, attemptId)
    {
    }

    public static Result<MatchAnswer, AppError> Create(
        Guid questionId, decimal pointsAwarded, AnswerCorrectness isCorrect, Guid attemptId)
    {
        var pointsResult = PointsAwarded.Create(pointsAwarded);
        if (pointsResult.IsFailure)
            return Result.Failure<MatchAnswer, AppError>(pointsResult.Error);

        return Result.Success<MatchAnswer, AppError>(
            new MatchAnswer(Guid.NewGuid(), questionId, pointsResult.Value, isCorrect, attemptId));
    }

    public UnitResult<AppError> AddUserMatch(Guid matchId)
    {
        if (_userMatches.Contains(matchId))
            return UnitResult.Failure<AppError>(new AppError("Match already selected."));

        _userMatches.Add(matchId);
        return UnitResult.Success<AppError>();
    }

    public UnitResult<AppError> RemoveUserMatch(Guid matchId)
    {
        if (!_userMatches.Remove(matchId))
            return UnitResult.Failure<AppError>(new AppError("Match not found in user selection."));

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