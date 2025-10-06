using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtoCommerce.AiHelper.Core.Models;
using VirtoCommerce.AiHelper.Data.Commands;
using Permissions = VirtoCommerce.AiHelper.Core.ModuleConstants.Security.Permissions;

namespace VirtoCommerce.AiHelper.Web.Controllers.Api;

[Authorize]
[Route("api/aihelper")]
public class AiHelperController : ControllerBase
{
    private readonly IMediator _mediator;

    public AiHelperController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("translate")]
    [Authorize(Permissions.Read)]
    public async Task<ActionResult<AiRequestResult>> Translate([FromBody] TranstlateCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }
}
