using Microsoft.AspNetCore.Mvc;
using QuestMasterService.Core.Features.Questions.CreateQuestion;
using QuestMasterService.Core.Questions.Commands.CreateTextQuestion;

namespace QuestMasterService.Web.Controllers.Tests;



[ApiController]
[Route("api/questions/text")]
public sealed class TextQuestionCreationController(
    CreateTextQuestionHandler handler)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateTextQuestionCommand command,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Created(new Uri($"/api/questions/{result.Value}"),
            new { id = result.Value });
    }
}
