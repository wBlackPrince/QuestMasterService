using CSharpFunctionalExtensions;

namespace QuestMasterService.Domain;

public sealed class Match
{
    public Guid Id { get; }
    public MatchLeft Left { get; private set; }
    public MatchRight Right { get; private set; }
    public bool IsCorrect { get; private set; }
    public Guid QuestionId { get; }

    private Match(Guid id, MatchLeft left, MatchRight right, bool isCorrect, Guid questionId)
    {
        Id = id;
        Left = left;
        Right = right;
        IsCorrect = isCorrect;
        QuestionId = questionId;
    }

    public static Result<Match, AppError> Create(string left, string right, bool isCorrect, Guid questionId)
    {
        var leftResult = MatchLeft.Create(left);
        if (leftResult.IsFailure)
            return Result.Failure<Match, AppError>(leftResult.Error);

        var rightResult = MatchRight.Create(right);
        if (rightResult.IsFailure)
            return Result.Failure<Match, AppError>(rightResult.Error);

        return Result.Success<Match, AppError>(
            new Match(Guid.NewGuid(), leftResult.Value, rightResult.Value, isCorrect, questionId));
    }

    public UnitResult<AppError> ChangeLeft(string newLeft)
    {
        var result = MatchLeft.Create(newLeft);
        if (result.IsFailure) return UnitResult.Failure<AppError>(result.Error);
        Left = result.Value;
        return UnitResult.Success<AppError>();
    }

    public UnitResult<AppError> ChangeRight(string newRight)
    {
        var result = MatchRight.Create(newRight);
        if (result.IsFailure) return UnitResult.Failure<AppError>(result.Error);
        Right = result.Value;
        return UnitResult.Success<AppError>();
    }

    public UnitResult<AppError> SetCorrectness(bool isCorrect)
    {
        IsCorrect = isCorrect;
        return UnitResult.Success<AppError>();
    }
}