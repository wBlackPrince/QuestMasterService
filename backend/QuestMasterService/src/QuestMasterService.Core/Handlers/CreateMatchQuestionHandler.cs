using CSharpFunctionalExtensions;
using QuestMasterService.Domain;
using QuestMasterService.Core.Abstractions.Repositories;
using QuestMasterService.Core.Features.Questions.CreateQuestion;

namespace QuestMasterService.Core.Questions.Commands.CreateMatchQuestion;

public sealed class CreateMatchQuestionHandler(
    ITestRepository testRepository,
    IQuestionRepository questionRepository,
    IMatchRepository matchRepository)
{
    public async Task<Result<Guid, AppError>> Handle(
        CreateMatchQuestionCommand command,
        CancellationToken cancellationToken = default)
    {
        var test = await testRepository.GetByIdAsync(
            command.TestId,
            cancellationToken);

        if (test is null)
            return Result.Failure<Guid, AppError>(
                new AppError("Test not found."));

        var descriptionResult = QuestionDescription.Create(command.Description);
        if (descriptionResult.IsFailure)
            return Result.Failure<Guid, AppError>(descriptionResult.Error);

        var pointsCostResult = PointsCost.Create(command.PointsCost);
        if (pointsCostResult.IsFailure)
            return Result.Failure<Guid, AppError>(pointsCostResult.Error);

        var positionResult = PositionIndex.Create(command.PositionIndex);
        if (positionResult.IsFailure)
            return Result.Failure<Guid, AppError>(positionResult.Error);

        var question = MatchQuestion.Create(
            Guid.NewGuid(),
            descriptionResult.Value,
            pointsCostResult.Value,
            command.TestId,
            positionResult.Value);

        if (question.IsFailure)
            return Result.Failure<Guid, AppError>(question.Error);

        foreach (var matchDto in command.Matches)
        {
            var matchResult = Match.Create(
                matchDto.Left,
                matchDto.Right,
                matchDto.IsCorrect,
                question.Value.Id);

            if (matchResult.IsFailure)
                return Result.Failure<Guid, AppError>(matchResult.Error);

            var addResult = question.Value.AddMatch(matchResult.Value.Id);

            if (addResult.IsFailure)
                return Result.Failure<Guid, AppError>(addResult.Error);

            await matchRepository.AddAsync(
                matchResult.Value,
                cancellationToken);
        }

        await questionRepository.AddAsync(
            question.Value,
            cancellationToken);

        await questionRepository.SaveChangesAsync(cancellationToken);

        return Result.Success<Guid, AppError>(question.Value.Id);
    }
}