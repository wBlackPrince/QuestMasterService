using CSharpFunctionalExtensions;

namespace QuestMasterService.Domain;

public sealed class OptionQuestion : Question
{
    private readonly List<Guid> _optionIds = new();
    public IReadOnlyList<Guid> Options => _optionIds.AsReadOnly();

    private OptionQuestion(Guid id, QuestionDescription description, PointsCost pointsCost,
        Guid testId, PositionIndex positionIndex)
        : base(id, description, QuestionType.Option, pointsCost, testId, positionIndex)
    {
    }

    public static Result<OptionQuestion, AppError> Create(
        Guid id, QuestionDescription description, PointsCost pointsCost,
        Guid testId, PositionIndex positionIndex)
    {
        return Result.Success<OptionQuestion, AppError>(
            new OptionQuestion(id, description, pointsCost, testId, positionIndex));
    }

    public UnitResult<AppError> AddOption(Guid optionId)
    {
        if (_optionIds.Contains(optionId))
            return UnitResult.Failure<AppError>(new AppError("Option already added to question."));

        _optionIds.Add(optionId);
        return UnitResult.Success<AppError>();
    }

    public UnitResult<AppError> RemoveOption(Guid optionId)
    {
        if (!_optionIds.Remove(optionId))
            return UnitResult.Failure<AppError>(new AppError("Option not found in question."));

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