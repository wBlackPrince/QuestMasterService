using CSharpFunctionalExtensions;
using QuestMasterService.Core.Abstractions.Repositories;
using QuestMasterService.Domain;

namespace QuestMasterService.Core.Features.Tests.DeleteTest;

public sealed class DeleteTestHandler(ITestRepository testRepository)
{
    public async Task<UnitResult<AppError>> Handle(
        DeleteTestCommand command,
        CancellationToken cancellationToken = default)
    {
        var test = await testRepository.GetByIdAsync(
            command.Id, cancellationToken);

        if (test is null)
            return UnitResult.Failure<AppError>(
                new AppError("Test not found."));

        testRepository.Remove(test);

        await testRepository.SaveChangesAsync(cancellationToken);

        return UnitResult.Success<AppError>();
    }
}