namespace QuestMasterService.Domain;

public abstract class Answer
{
    public Guid Id { get; }
    public AnswerType Type { get; }
    public Guid QuestionId { get; }
    public PointsAwarded PointsAwarded { get; protected set; }
    public AnswerCorrectness IsCorrect { get; protected set; }
    public Guid AttemptId { get; }

    protected Answer()
    {
        PointsAwarded = null!;
    }

    protected Answer(Guid id, AnswerType type, Guid questionId,
        PointsAwarded pointsAwarded, AnswerCorrectness isCorrect, Guid attemptId)
    {
        Id = id;
        Type = type;
        QuestionId = questionId;
        PointsAwarded = pointsAwarded;
        IsCorrect = isCorrect;
        AttemptId = attemptId;
    }
}
