using Microsoft.AspNetCore.Mvc;
using QuestMasterService.Core.Features.Questions.CreateQuestion;
using QuestMasterService.Core.Questions.Commands.CreateMatchQuestion;

namespace QuestMasterService.Web.Controllers.Tests;

[ApiController]
[Route("api/questions/match")]
public sealed class MatchQuestionCreationController(
    CreateMatchQuestionHandler handler)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateMatchQuestionCommand command,
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