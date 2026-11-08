using CSharpFunctionalExtensions;
using QuestMasterService.Core.Abstractions.Repositories;
using QuestMasterService.Core.Features.Questions.CreateQuestion;
using QuestMasterService.Domain;

namespace QuestMasterService.Core.Questions.Commands.CreateTextQuestion;

public sealed class CreateTextQuestionHandler(
    ITestRepository testRepository,
    IQuestionRepository questionRepository)
{
    public async Task<Result<Guid, AppError>> Handle(
        CreateTextQuestionCommand command,
        CancellationToken cancellationToken = default)
    {
        var test = await testRepository.GetByIdAsync(
            command.TestId,
            cancellationToken);

        if (test is null)
            return Result.Failure<Guid, AppError>(
                new AppError("Test not found."));

        var question = TextQuestion.Create(
            Guid.NewGuid(),
            QuestionDescription.Create(command.Description).Value,
            PointsCost.Create(command.PointsCost).Value,
            command.TestId,
            PositionIndex.Create(command.PositionIndex).Value,
            command.RightAnswer);

        if (question.IsFailure)
            return Result.Failure<Guid, AppError>(question.Error);

        await questionRepository.AddAsync(
            question.Value,
            cancellationToken);

        await questionRepository.SaveChangesAsync(cancellationToken);

        return Result.Success<Guid, AppError>(question.Value.Id);
    }
}