using CSharpFunctionalExtensions;
using QuestMasterService.Core.Abstractions.Repositories;
using QuestMasterService.Domain;

namespace QuestMasterService.Core.Features.Tests.GetTestById;

public sealed class GetTestByIdHandler(ITestRepository testRepository)
{
    public async Task<Result<Test, AppError>> Handle(
        GetTestByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var test = await testRepository.GetByIdAsync(
            query.Id, cancellationToken);

        if (test is null)
            return Result.Failure<Test, AppError>(
                new AppError("Test not found."));

        return Result.Success<Test, AppError>(test);
    }
}