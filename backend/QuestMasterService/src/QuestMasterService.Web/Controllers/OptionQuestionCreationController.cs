using Microsoft.AspNetCore.Mvc;
using QuestMasterService.Core.Features.Questions.CreateQuestion;
using QuestMasterService.Core.Questions.Commands.CreateOptionQuestion;

namespace QuestMasterService.Web.Controllers.Tests;

[ApiController]
[Route("api/questions/options")]
public sealed class OptionQuestionCreationController(
    CreateOptionQuestionHandler handler)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateOptionQuestionCommand command,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return CreatedAtRoute(
            "GetQuestionById",
            new { id = result.Value },
            new { id = result.Value });
    }
}
