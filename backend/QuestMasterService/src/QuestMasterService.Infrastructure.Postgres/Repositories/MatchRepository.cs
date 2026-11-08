using QuestMasterService.Core.Abstractions.Repositories;
using QuestMasterService.Domain;

namespace QuestMasterService.Infrastructure.Postgres.Repositories;

public sealed class MatchRepository(QuestMasterDbContext dbContext)
    : IMatchRepository
{
    public Task AddAsync(
        Match match,
        CancellationToken cancellationToken = default)
    {
        return dbContext.Matches.AddAsync(match, cancellationToken).AsTask();
    }
}