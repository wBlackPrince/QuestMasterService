using Microsoft.EntityFrameworkCore;
using QuestMasterService.Core.Abstractions.Repositories;
using QuestMasterService.Domain;

namespace QuestMasterService.Infrastructure.Postgres.Repositories;

public sealed class QuestionRepository(QuestMasterDbContext dbContext)
    : IQuestionRepository
{
    public async Task AddAsync(
        Question question,
        CancellationToken cancellationToken = default)
    {
        await dbContext.Questions.AddAsync(question, cancellationToken);
    }

    public async Task<Question?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Questions
            .FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Question>> GetByTestIdAsync(
        Guid testId,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Questions
            .AsNoTracking()
            .Where(q => q.TestId == testId)
            .OrderBy(q => q.PositionIndex)
            .ToListAsync(cancellationToken);
    }

    public void Remove(Question question)
    {
        dbContext.Questions.Remove(question);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}