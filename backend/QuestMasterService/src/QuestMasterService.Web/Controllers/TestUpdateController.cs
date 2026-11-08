using Microsoft.AspNetCore.Mvc;
using QuestMasterService.Core.Features.Tests.UpdateTest;

namespace QuestMasterService.Web.Controllers.Tests;

[ApiController]
[Route("api/tests")]
public sealed class TestUpdateController(
    UpdateTestHandler handler) : ControllerBase
{
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateTestCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest(new { error = "ID mismatch." });

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
        {
            if (string.Equals(
                    result.Error.Message,
                    "Test not found.",
                    StringComparison.Ordinal))
            {
                return NotFound(new { error = result.Error.Message });
            }

            return BadRequest(new { error = result.Error.Message });
        }

        return NoContent();
    }
}