using CSharpFunctionalExtensions;
using QuestMasterService.Core.Abstractions.Repositories;
using QuestMasterService.Domain;

namespace QuestMasterService.Core.Features.Tests.CreateTest;

public sealed class CreateTestHandler(ITestRepository testRepository)
{
    public async Task<Result<Guid, AppError>> Handle(
        CreateTestCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = Test.Create(
            command.Title,
            command.Description,
            command.MaxAttempts);

        if (result.IsFailure)
            return Result.Failure<Guid, AppError>(result.Error);

        var test = result.Value;

        await testRepository.AddAsync(test, cancellationToken);
        await testRepository.SaveChangesAsync(cancellationToken);

        return Result.Success<Guid, AppError>(test.Id);
    }
}