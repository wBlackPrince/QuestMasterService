using Microsoft.AspNetCore.Mvc;
using QuestMasterService.Core.Features.Tests.DeleteTest;

namespace QuestMasterService.Web.Controllers.Tests;

[ApiController]
[Route("api/tests")]
public sealed class TestDeletionController(
    DeleteTestHandler handler) : ControllerBase
{
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(
            new DeleteTestCommand(id), cancellationToken);

        if (result.IsFailure)
            return NotFound(new { error = result.Error.Message });

        return NoContent();
    }
}