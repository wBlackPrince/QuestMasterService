using CSharpFunctionalExtensions;

namespace QuestMasterService.Domain;

public sealed class TextQuestion : Question
{
    public RightAnswerText RightAnswer { get; private set; } = default!;

    // EF Core
    private TextQuestion() { }

    private TextQuestion(Guid id, QuestionDescription description, PointsCost pointsCost,
        Guid testId, PositionIndex positionIndex, RightAnswerText rightAnswer)
        : base(id, description, QuestionType.Text, pointsCost, testId, positionIndex)
    {
        RightAnswer = rightAnswer;
    }

    public static Result<TextQuestion, AppError> Create(
        Guid id, QuestionDescription description, PointsCost pointsCost,
        Guid testId, PositionIndex positionIndex, string rightAnswer)
    {
        var rightAnswerResult = RightAnswerText.Create(rightAnswer);
        if (rightAnswerResult.IsFailure)
            return Result.Failure<TextQuestion, AppError>(rightAnswerResult.Error);

        return Result.Success<TextQuestion, AppError>(
            new TextQuestion(id, description, pointsCost, testId, positionIndex, rightAnswerResult.Value));
    }

    public static Result<TextQuestion, AppError> Create(
        Guid id, QuestionDescription description, PointsCost pointsCost,
        Guid testId, PositionIndex positionIndex)
    {
        // Для создания без rightAnswer (заполняется позже)
        var emptyRightAnswer = RightAnswerText.Create(string.Empty);
        if (emptyRightAnswer.IsFailure)
            return Result.Failure<TextQuestion, AppError>(emptyRightAnswer.Error);

        return Result.Success<TextQuestion, AppError>(
            new TextQuestion(id, description, pointsCost, testId, positionIndex, emptyRightAnswer.Value));
    }

    public UnitResult<AppError> SetRightAnswer(string rightAnswer)
    {
        var result = RightAnswerText.Create(rightAnswer);
        if (result.IsFailure) return UnitResult.Failure<AppError>(result.Error);
        RightAnswer = result.Value;
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