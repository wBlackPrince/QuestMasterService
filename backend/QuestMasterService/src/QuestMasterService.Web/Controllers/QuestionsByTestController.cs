using Microsoft.AspNetCore.Mvc;
using QuestMasterService.Core.Features.Questions.GetQuestionsByTest;

namespace QuestMasterService.Web.Controllers;

[ApiController]
[Route("api/questions")]
public sealed class QuestionsByTestController(
    GetQuestionsByTestHandler handler)
    : ControllerBase
{
    [HttpGet("by-test/{testId:guid}")]
    public async Task<IActionResult> GetByTest(
        Guid testId,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(
            new GetQuestionsByTestQuery(testId),
            cancellationToken);

        return Ok(result);
    }
}