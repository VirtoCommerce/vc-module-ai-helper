using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtoCommerce.AiHelper.Core.Models;
using VirtoCommerce.AiHelper.Data.Queries;
using static VirtoCommerce.AiHelper.Core.ModuleConstants.Security;

namespace VirtoCommerce.AiHelper.Web.Controllers.Api;

[Authorize]
[Route("api/aihelper")]
public class AiHelperCommonController : ControllerBase
{
    private readonly IMediator _mediator;

    public AiHelperCommonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("settings")]
    [Authorize(Permissions.Read)]
    public async Task<ActionResult<AiHelperSettings>> GetSettings()
    {
        var query = new GetAiHelperSettingsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
