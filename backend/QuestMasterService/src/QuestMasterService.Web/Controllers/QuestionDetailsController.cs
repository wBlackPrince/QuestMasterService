using Microsoft.AspNetCore.Mvc;
using QuestMasterService.Core.Features.Questions.GetQuestionById;

namespace QuestMasterService.Web.Controllers;

[ApiController]
[Route("api/questions")]
public sealed class QuestionDetailsController(
    GetQuestionByIdHandler handler)
    : ControllerBase
{
    [HttpGet("{id:guid}", Name = "GetQuestionById")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(
            new GetQuestionByIdQuery(id),
            cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }
}
