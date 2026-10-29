namespace QuestMasterService.Domain;

public abstract class Question
{
    public Guid Id { get; }
    public QuestionDescription Description { get; protected set; }
    public QuestionType Type { get; }
    public PointsCost PointsCost { get; protected set; }
    public Guid TestId { get; }
    public PositionIndex PositionIndex { get; protected set; }

    protected Question(Guid id, QuestionDescription description, QuestionType type,
        PointsCost pointsCost, Guid testId, PositionIndex positionIndex)
    {
        Id = id;
        Description = description;
        Type = type;
        PointsCost = pointsCost;
        TestId = testId;
        PositionIndex = positionIndex;
    }
}