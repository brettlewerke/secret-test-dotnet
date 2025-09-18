using secret_test.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace secret_test.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class InsightController : ControllerBase
{
    private readonly IInsightService _service;

    public InsightController(ILogger<InsightController> logger, IInsightService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<string>> Get()
    {
        IEnumerable<string> returnValue = _service.GetDescriptions();
        return new OkObjectResult(returnValue);
    }
}
