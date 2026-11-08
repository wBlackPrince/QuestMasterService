using QuestMasterService.Domain;

namespace QuestMasterService.Core.Abstractions.Repositories;

public interface ITestRepository
{
    Task AddAsync(
        Test test,
        CancellationToken cancellationToken = default);

    Task<Test?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Test>> GetAllAsync(
        CancellationToken cancellationToken = default);

    void Remove(Test test);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}