using QuestMasterService.Domain;

namespace QuestMasterService.Core.Abstractions.Repositories;

public interface IQuestionRepository
{
    Task AddAsync(
        Question question,
        CancellationToken cancellationToken = default);

    Task<Question?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Question>> GetByTestIdAsync(
        Guid testId,
        CancellationToken cancellationToken = default);

    void Remove(Question question);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}