using CSharpFunctionalExtensions;
using QuestMasterService.Core.Abstractions.Repositories;
using QuestMasterService.Domain;

namespace QuestMasterService.Core.Features.Questions.DeleteQuestion;

public sealed class DeleteQuestionHandler(
    IQuestionRepository questionRepository)
{
    public async Task<UnitResult<AppError>> Handle(
        DeleteQuestionCommand command,
        CancellationToken cancellationToken = default)
    {
        var question = await questionRepository.GetByIdAsync(
            command.Id, cancellationToken);

        if (question is null)
            return UnitResult.Failure<AppError>(
                new AppError("Question not found."));

        questionRepository.Remove(question);

        await questionRepository.SaveChangesAsync(cancellationToken);

        return UnitResult.Success<AppError>();
    }
}