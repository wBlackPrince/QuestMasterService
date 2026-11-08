using QuestMasterService.Domain;

namespace QuestMasterService.Core.Abstractions.Persistence;

public interface IQuestMasterDbContext
{
    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);
}