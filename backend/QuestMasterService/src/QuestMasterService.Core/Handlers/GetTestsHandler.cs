using QuestMasterService.Core.Abstractions.Repositories;
using QuestMasterService.Domain;

namespace QuestMasterService.Core.Features.Tests.GetTests;

public sealed class GetTestsHandler(ITestRepository testRepository)
{
    public async Task<IReadOnlyList<Test>> Handle(
        GetTestsQuery query,
        CancellationToken cancellationToken = default)
    {
        return await testRepository.GetAllAsync(cancellationToken);
    }
}