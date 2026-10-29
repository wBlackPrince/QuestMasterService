using CSharpFunctionalExtensions;

namespace QuestMasterService.Domain;

public sealed class TextAnswer : Answer
{
    public UserAnswerText UserAnswer { get; private set; }

    private TextAnswer(Guid id, Guid questionId, PointsAwarded pointsAwarded,
        AnswerCorrectness isCorrect, Guid attemptId, UserAnswerText userAnswer)
        : base(id, AnswerType.Text, questionId, pointsAwarded, isCorrect, attemptId)
    {
        UserAnswer = userAnswer;
    }

    public static Result<TextAnswer, AppError> Create(
        Guid questionId, decimal pointsAwarded, AnswerCorrectness isCorrect,
        Guid attemptId, string userAnswer)
    {
        var pointsResult = PointsAwarded.Create(pointsAwarded);
        if (pointsResult.IsFailure)
            return Result.Failure<TextAnswer, AppError>(pointsResult.Error);

        var userAnswerResult = UserAnswerText.Create(userAnswer);
        if (userAnswerResult.IsFailure)
            return Result.Failure<TextAnswer, AppError>(userAnswerResult.Error);

        return Result.Success<TextAnswer, AppError>(
            new TextAnswer(Guid.NewGuid(), questionId, pointsResult.Value, isCorrect, attemptId, userAnswerResult.Value));
    }

    public UnitResult<AppError> ChangeUserAnswer(string newUserAnswer)
    {
        var result = UserAnswerText.Create(newUserAnswer);
        if (result.IsFailure) return UnitResult.Failure<AppError>(result.Error);
        UserAnswer = result.Value;
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