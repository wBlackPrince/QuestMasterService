using Microsoft.EntityFrameworkCore;
using QuestMasterService.Core.Abstractions.Repositories;
using QuestMasterService.Domain;

namespace QuestMasterService.Infrastructure.Postgres.Repositories;

public sealed class TestRepository(QuestMasterDbContext dbContext)
    : ITestRepository
{
    public async Task AddAsync(
        Test test,
        CancellationToken cancellationToken = default)
    {
        await dbContext.Tests.AddAsync(test, cancellationToken);
    }

    public async Task<Test?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Tests
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Test>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Tests
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public void Remove(Test test)
    {
        dbContext.Tests.Remove(test);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}