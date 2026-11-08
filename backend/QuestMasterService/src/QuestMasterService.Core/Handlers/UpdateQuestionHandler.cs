using CSharpFunctionalExtensions;
using QuestMasterService.Core.Abstractions.Repositories;
using QuestMasterService.Domain;

namespace QuestMasterService.Core.Features.Questions.UpdateQuestion;

public sealed class UpdateQuestionHandler(
    IQuestionRepository questionRepository)
{
    public async Task<UnitResult<AppError>> Handle(
        UpdateQuestionCommand command,
        CancellationToken cancellationToken = default)
    {
        var question = await questionRepository.GetByIdAsync(
            command.Id, cancellationToken);

        if (question is null)
            return UnitResult.Failure<AppError>(
                new AppError("Question not found."));

        var descriptionResult = question switch
        {
            TextQuestion q => q.ChangeDescription(command.Description),
            OptionQuestion q => q.ChangeDescription(command.Description),
            MatchQuestion q => q.ChangeDescription(command.Description),
            _ => UnitResult.Failure<AppError>(
                new AppError("Unsupported question type."))
        };

        if (descriptionResult.IsFailure)
            return descriptionResult;

        var pointsResult = question switch
        {
            TextQuestion q => q.ChangePointsCost(command.PointsCost),
            OptionQuestion q => q.ChangePointsCost(command.PointsCost),
            MatchQuestion q => q.ChangePointsCost(command.PointsCost),
            _ => UnitResult.Failure<AppError>(
                new AppError("Unsupported question type."))
        };

        if (pointsResult.IsFailure)
            return pointsResult;

        var positionResult = question switch
        {
            TextQuestion q => q.ChangePositionIndex(command.PositionIndex),
            OptionQuestion q => q.ChangePositionIndex(command.PositionIndex),
            MatchQuestion q => q.ChangePositionIndex(command.PositionIndex),
            _ => UnitResult.Failure<AppError>(
                new AppError("Unsupported question type."))
        };

        if (positionResult.IsFailure)
            return positionResult;

        await questionRepository.SaveChangesAsync(cancellationToken);

        return UnitResult.Success<AppError>();
    }
}