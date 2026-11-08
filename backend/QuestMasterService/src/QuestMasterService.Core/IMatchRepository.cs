using QuestMasterService.Domain;

namespace QuestMasterService.Core.Abstractions.Repositories;

public interface IMatchRepository
{
    Task AddAsync(
        Match match,
        CancellationToken cancellationToken = default);
}