using CSharpFunctionalExtensions;
using QuestMasterService.Core.Abstractions.Repositories;
using QuestMasterService.Core.Features.Questions.CreateQuestion;
using QuestMasterService.Domain;

namespace QuestMasterService.Core.Questions.Commands.CreateOptionQuestion;

public sealed class CreateOptionQuestionHandler(
    ITestRepository testRepository,
    IQuestionRepository questionRepository,
    IQuestionOptionRepository questionOptionRepository)
{
    public async Task<Result<Guid, AppError>> Handle(
        CreateOptionQuestionCommand command,
        CancellationToken cancellationToken = default)
    {
        var test = await testRepository.GetByIdAsync(
            command.TestId,
            cancellationToken);

        if (test is null)
            return Result.Failure<Guid, AppError>(
                new AppError("Test not found."));

        var description = QuestionDescription.Create(command.Description);
        if (description.IsFailure)
            return Result.Failure<Guid, AppError>(description.Error);

        var pointsCost = PointsCost.Create(command.PointsCost);
        if (pointsCost.IsFailure)
            return Result.Failure<Guid, AppError>(pointsCost.Error);

        var position = PositionIndex.Create(command.PositionIndex);
        if (position.IsFailure)
            return Result.Failure<Guid, AppError>(position.Error);

        var question = OptionQuestion.Create(
            Guid.NewGuid(),
            description.Value,
            pointsCost.Value,
            command.TestId,
            position.Value);

        if (question.IsFailure)
            return Result.Failure<Guid, AppError>(question.Error);

        foreach (var optionDto in command.Options)
        {
            var option = QuestionOption.Create(
                optionDto.Text,
                optionDto.IsCorrect,
                question.Value.Id);

            if (option.IsFailure)
                return Result.Failure<Guid, AppError>(option.Error);

            var addResult = question.Value.AddOption(option.Value.Id);

            if (addResult.IsFailure)
                return Result.Failure<Guid, AppError>(addResult.Error);

            await questionOptionRepository.AddAsync(
                option.Value,
                cancellationToken);
        }

        await questionRepository.AddAsync(
            question.Value,
            cancellationToken);

        await questionRepository.SaveChangesAsync(cancellationToken);

        return Result.Success<Guid, AppError>(question.Value.Id);
    }
}