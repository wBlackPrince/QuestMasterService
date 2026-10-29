using Microsoft.AspNetCore.Mvc;

namespace QuestMasterService.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TestController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<TestResponse>(StatusCodes.Status200OK)]
    public ActionResult<TestResponse> Get()
    {
        return Ok(new TestResponse("Test controller is working.", DateTimeOffset.UtcNow));
    }
}

public sealed record TestResponse(string Message, DateTimeOffset Timestamp);
