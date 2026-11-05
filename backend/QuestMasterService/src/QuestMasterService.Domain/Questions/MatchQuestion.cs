using CSharpFunctionalExtensions;

namespace QuestMasterService.Domain;

public sealed class MatchQuestion : Question
{
    private readonly List<Guid> _matchIds = new();
    public IReadOnlyList<Guid> Matches => _matchIds.AsReadOnly();

    // EF Core
    private MatchQuestion() { }

    private MatchQuestion(Guid id, QuestionDescription description, PointsCost pointsCost,
        Guid testId, PositionIndex positionIndex)
        : base(id, description, QuestionType.Match, pointsCost, testId, positionIndex)
    {
    }

    public static Result<MatchQuestion, AppError> Create(
        Guid id, QuestionDescription description, PointsCost pointsCost,
        Guid testId, PositionIndex positionIndex)
    {
        return Result.Success<MatchQuestion, AppError>(
            new MatchQuestion(id, description, pointsCost, testId, positionIndex));
    }

    public UnitResult<AppError> AddMatch(Guid matchId)
    {
        if (_matchIds.Contains(matchId))
            return UnitResult.Failure<AppError>(new AppError("Match already added to question."));

        _matchIds.Add(matchId);
        return UnitResult.Success<AppError>();
    }

    public UnitResult<AppError> RemoveMatch(Guid matchId)
    {
        if (!_matchIds.Remove(matchId))
            return UnitResult.Failure<AppError>(new AppError("Match not found in question."));

        return UnitResult.Success<AppError>();
    }

    public UnitResult<AppError> ChangeDescription(string newDescription)
    {
        var result = QuestionDescription.Create(newDescription);
        if (result.IsFailure) return UnitResult.Failure<AppError>(result.Error);
        Description = result.Value;
        return UnitResult.Success<AppError>();
    }

    public UnitResult<AppError> ChangePointsCost(decimal newPointsCost)
    {
        var result = PointsCost.Create(newPointsCost);
        if (result.IsFailure) return UnitResult.Failure<AppError>(result.Error);
        PointsCost = result.Value;
        return UnitResult.Success<AppError>();
    }

    public UnitResult<AppError> ChangePositionIndex(int newPositionIndex)
    {
        var result = PositionIndex.Create(newPositionIndex);
        if (result.IsFailure) return UnitResult.Failure<AppError>(result.Error);
        PositionIndex = result.Value;
        return UnitResult.Success<AppError>();
    }
}