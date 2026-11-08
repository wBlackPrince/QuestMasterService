using QuestMasterService.Core.Abstractions.Repositories;
using QuestMasterService.Domain;

namespace QuestMasterService.Core.Features.Questions.GetQuestionsByTest;

public sealed class GetQuestionsByTestHandler(
    IQuestionRepository questionRepository)
{
    public async Task<IReadOnlyList<Question>> Handle(
        GetQuestionsByTestQuery query,
        CancellationToken cancellationToken = default)
    {
        return await questionRepository.GetByTestIdAsync(
            query.TestId, cancellationToken);
    }
}