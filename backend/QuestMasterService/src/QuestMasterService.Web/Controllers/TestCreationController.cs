using Microsoft.AspNetCore.Mvc;
using QuestMasterService.Core.Features.Tests.CreateTest;

namespace QuestMasterService.Web.Controllers.Tests;

[ApiController]
[Route("api/tests")]
public sealed class TestCreationController(
    CreateTestHandler handler) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateTestCommand command,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Message });

        return CreatedAtRoute(
            "GetTestById",
            new { id = result.Value },
            new { id = result.Value });
    }
}