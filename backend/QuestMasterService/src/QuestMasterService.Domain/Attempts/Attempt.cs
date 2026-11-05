using CSharpFunctionalExtensions;

namespace QuestMasterService.Domain;

public sealed class Attempt
{
    public Guid Id { get; }
    public Guid TestId { get; }
    public Guid UserId { get; }
    public DateTime StartedAt { get; }
    public DateTime? FinishedAt { get; private set; }
    public TotalScore? TotalScore { get; private set; }

    private readonly List<Answer> _answers = new();
    public IReadOnlyList<Answer> Answers => _answers.AsReadOnly();

    // EF Core
    private Attempt() { }

    private Attempt(Guid id, Guid testId, Guid userId, DateTime startedAt)
    {
        Id = id;
        TestId = testId;
        UserId = userId;
        StartedAt = startedAt;
    }

    public static Result<Attempt, AppError> Create(Guid testId, Guid userId, DateTime? startedAt = null)
    {
        var attempt = new Attempt(Guid.NewGuid(), testId, userId, startedAt ?? DateTime.UtcNow);
        return Result.Success<Attempt, AppError>(attempt);
    }

    public Result<Answer, AppError> AddTextAnswer(Guid questionId, decimal pointsAwarded,
        AnswerCorrectness isCorrect, string userAnswer)
    {
        if (FinishedAt.HasValue)
            return Result.Failure<Answer, AppError>(new AppError("Cannot add answers to a finished attempt."));

        var answerResult = TextAnswer.Create(questionId, pointsAwarded, isCorrect, Id, userAnswer);
        if (answerResult.IsFailure)
            return Result.Failure<Answer, AppError>(answerResult.Error);

        _answers.Add(answerResult.Value);
        return Result.Success<Answer, AppError>(answerResult.Value);
    }

    public Result<Answer, AppError> AddMatchAnswer(Guid questionId, decimal pointsAwarded,
        AnswerCorrectness isCorrect)
    {
        if (FinishedAt.HasValue)
            return Result.Failure<Answer, AppError>(new AppError("Cannot add answers to a finished attempt."));

        var answerResult = MatchAnswer.Create(questionId, pointsAwarded, isCorrect, Id);
        if (answerResult.IsFailure)
            return Result.Failure<Answer, AppError>(answerResult.Error);

        _answers.Add(answerResult.Value);
        return Result.Success<Answer, AppError>(answerResult.Value);
    }

    public Result<Answer, AppError> AddOptionsAnswer(Guid questionId, decimal pointsAwarded,
        AnswerCorrectness isCorrect)
    {
        if (FinishedAt.HasValue)
            return Result.Failure<Answer, AppError>(new AppError("Cannot add answers to a finished attempt."));

        var answerResult = OptionsAnswer.Create(questionId, pointsAwarded, isCorrect, Id);
        if (answerResult.IsFailure)
            return Result.Failure<Answer, AppError>(answerResult.Error);

        _answers.Add(answerResult.Value);
        return Result.Success<Answer, AppError>(answerResult.Value);
    }

    public UnitResult<AppError> Finish(decimal totalScore)
    {
        if (FinishedAt.HasValue)
            return UnitResult.Failure<AppError>(new AppError("Attempt is already finished."));

        var totalScoreResult = TotalScore.Create(totalScore);
        if (totalScoreResult.IsFailure)
            return UnitResult.Failure<AppError>(totalScoreResult.Error);

        FinishedAt = DateTime.UtcNow;
        TotalScore = totalScoreResult.Value;

        return UnitResult.Success<AppError>();
    }
}