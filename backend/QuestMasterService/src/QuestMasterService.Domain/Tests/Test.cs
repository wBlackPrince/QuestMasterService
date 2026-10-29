using CSharpFunctionalExtensions;

namespace QuestMasterService.Domain;

public sealed class Test
{
    public Guid Id { get; }
    public TestTitle Title { get; private set; }
    public TestDescription Description { get; private set; }
    public MaxAttempts MaxAttempts { get; private set; }

    private readonly List<Question> _questions = new();
    public IReadOnlyList<Question> Questions => _questions.AsReadOnly();

    private Test(Guid id, TestTitle title, TestDescription description, MaxAttempts maxAttempts)
    {
        Id = id;
        Title = title;
        Description = description;
        MaxAttempts = maxAttempts;
    }

    public static Result<Test, AppError> Create(string title, string description, int maxAttempts)
    {
        var titleResult = TestTitle.Create(title);
        if (titleResult.IsFailure)
            return Result.Failure<Test, AppError>(titleResult.Error);

        var descResult = TestDescription.Create(description);
        if (descResult.IsFailure)
            return Result.Failure<Test, AppError>(descResult.Error);

        var maxAttemptsResult = MaxAttempts.Create(maxAttempts);
        if (maxAttemptsResult.IsFailure)
            return Result.Failure<Test, AppError>(maxAttemptsResult.Error);

        return Result.Success<Test, AppError>(
            new Test(Guid.NewGuid(), titleResult.Value, descResult.Value, maxAttemptsResult.Value));
    }

    public UnitResult<AppError> ChangeTitle(string newTitle)
    {
        var result = TestTitle.Create(newTitle);
        if (result.IsFailure) return UnitResult.Failure<AppError>(result.Error);
        Title = result.Value;
        return UnitResult.Success<AppError>();
    }

    public UnitResult<AppError> ChangeDescription(string newDescription)
    {
        var result = TestDescription.Create(newDescription);
        if (result.IsFailure) return UnitResult.Failure<AppError>(result.Error);
        Description = result.Value;
        return UnitResult.Success<AppError>();
    }

    public UnitResult<AppError> ChangeMaxAttempts(int newMaxAttempts)
    {
        var result = MaxAttempts.Create(newMaxAttempts);
        if (result.IsFailure) return UnitResult.Failure<AppError>(result.Error);
        MaxAttempts = result.Value;
        return UnitResult.Success<AppError>();
    }

    public Result<Question, AppError> AddQuestion(
        QuestionType type, string description, decimal pointsCost, int positionIndex)
    {
        var descResult = QuestionDescription.Create(description);
        if (descResult.IsFailure)
            return Result.Failure<Question, AppError>(descResult.Error);

        var pointsResult = PointsCost.Create(pointsCost);
        if (pointsResult.IsFailure)
            return Result.Failure<Question, AppError>(pointsResult.Error);

        var posResult = PositionIndex.Create(positionIndex);
        if (posResult.IsFailure)
            return Result.Failure<Question, AppError>(posResult.Error);

        Question question = type switch
        {
            QuestionType.Text => TextQuestion.Create(Guid.NewGuid(), descResult.Value, pointsResult.Value, Id, posResult.Value).Value,
            QuestionType.Match => MatchQuestion.Create(Guid.NewGuid(), descResult.Value, pointsResult.Value, Id, posResult.Value).Value,
            QuestionType.Option => OptionQuestion.Create(Guid.NewGuid(), descResult.Value, pointsResult.Value, Id, posResult.Value).Value,
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };

        _questions.Add(question);
        return Result.Success<Question, AppError>(question);
    }

    public UnitResult<AppError> RemoveQuestion(Guid questionId)
    {
        var question = _questions.FirstOrDefault(q => q.Id == questionId);
        if (question is null)
            return UnitResult.Failure<AppError>(new AppError("Question not found in test."));

        _questions.Remove(question);
        return UnitResult.Success<AppError>();
    }
}