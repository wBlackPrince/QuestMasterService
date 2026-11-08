using QuestMasterService.Core.Abstractions.Repositories;
using QuestMasterService.Domain;

namespace QuestMasterService.Infrastructure.Postgres.Repositories;

public sealed class QuestionOptionRepository(
    QuestMasterDbContext dbContext)
    : IQuestionOptionRepository
{
    public Task AddAsync(
        QuestionOption questionOption,
        CancellationToken cancellationToken = default)
    {
        return dbContext.QuestionOptions
            .AddAsync(questionOption, cancellationToken).AsTask();
    }
}