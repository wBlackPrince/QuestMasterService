using Microsoft.AspNetCore.Mvc;
using QuestMasterService.Core.Features.Tests.GetTests;

namespace QuestMasterService.Web.Controllers.Tests;

[ApiController]
[Route("api/tests")]
public sealed class TestListController(
    GetTestsHandler handler) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(
            new GetTestsQuery(), cancellationToken);

        return Ok(result);
    }
}