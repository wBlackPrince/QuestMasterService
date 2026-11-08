using Microsoft.AspNetCore.Mvc;
using QuestMasterService.Core.Features.Tests.GetTestById;

namespace QuestMasterService.Web.Controllers.Tests;

[ApiController]
[Route("api/tests")]
public sealed class TestDetailsController(
    GetTestByIdHandler handler) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(
            new GetTestByIdQuery(id), cancellationToken);

        if (result.IsFailure)
            return NotFound(new { error = result.Error.Message });

        return Ok(result.Value);
    }
}