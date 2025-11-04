using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtoCommerce.AiHelper.Core.Models;
using VirtoCommerce.AiHelper.Core.Models.Search;
using VirtoCommerce.AiHelper.Data.Queries;
using static VirtoCommerce.AiHelper.Core.ModuleConstants.Security;

namespace VirtoCommerce.AiHelper.Web.Controllers.Api;

[Authorize]
[Route("api/aihelper")]
public class AiHelperLogController : ControllerBase
{
    private readonly IMediator _mediator;

    public AiHelperLogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("log/search")]
    [Authorize(Permissions.Read)]
    public async Task<ActionResult<AiRequestLogSearchResult>> SearchLog([FromBody] SearchAiRequestLogQuery query)
    {
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet]
    [Route("log/{logId}")]
    [Authorize(Permissions.Read)]
    public async Task<ActionResult<AiRequestLog>> GetAiRequestLogById([FromRoute] string logId, [FromQuery] string logLevel = null)
    {
        var query = new GetAiRequestLogQuery { LogId = logId, ResponseGroup = logLevel };
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
