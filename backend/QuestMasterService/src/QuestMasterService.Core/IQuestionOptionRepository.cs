using QuestMasterService.Domain;

namespace QuestMasterService.Core.Abstractions.Repositories;

public interface IQuestionOptionRepository
{
    Task AddAsync(
        QuestionOption questionOption,
        CancellationToken cancellationToken = default);
}