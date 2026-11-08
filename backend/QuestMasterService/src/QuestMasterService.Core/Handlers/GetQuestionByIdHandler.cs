using CSharpFunctionalExtensions;
using QuestMasterService.Core.Abstractions.Repositories;
using QuestMasterService.Domain;

namespace QuestMasterService.Core.Features.Questions.GetQuestionById;

public sealed class GetQuestionByIdHandler(
    IQuestionRepository questionRepository)
{
    public async Task<Result<Question, AppError>> Handle(
        GetQuestionByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var question = await questionRepository.GetByIdAsync(
            query.Id, cancellationToken);

        if (question is null)
            return Result.Failure<Question, AppError>(
                new AppError("Question not found."));

        return Result.Success<Question, AppError>(question);
    }
}