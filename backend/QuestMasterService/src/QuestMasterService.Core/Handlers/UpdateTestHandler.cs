using CSharpFunctionalExtensions;
using QuestMasterService.Core.Abstractions.Repositories;
using QuestMasterService.Domain;

namespace QuestMasterService.Core.Features.Tests.UpdateTest;

public sealed class UpdateTestHandler(ITestRepository testRepository)
{
    public async Task<UnitResult<AppError>> Handle(
        UpdateTestCommand command,
        CancellationToken cancellationToken = default)
    {
        var test = await testRepository.GetByIdAsync(
            command.Id, cancellationToken);

        if (test is null)
            return UnitResult.Failure<AppError>(
                new AppError("Test not found."));

        var titleResult = test.ChangeTitle(command.Title);

        if (titleResult.IsFailure)
            return titleResult;

        var descriptionResult = test.ChangeDescription(command.Description);

        if (descriptionResult.IsFailure)
            return descriptionResult;

        var attemptsResult = test.ChangeMaxAttempts(command.MaxAttempts);

        if (attemptsResult.IsFailure)
            return attemptsResult;

        await testRepository.SaveChangesAsync(cancellationToken);

        return UnitResult.Success<AppError>();
    }
}