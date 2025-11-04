using System;
using System.Threading;
using System.Threading.Tasks;
using VirtoCommerce.AiHelper.Core.Common;
using VirtoCommerce.AiHelper.Core.Models;
using VirtoCommerce.AiHelper.Core.Services;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.AiHelper.Data.Queries;
public class GetAiRequestLogQueryHandler : IQueryHandler<GetAiRequestLogQuery, AiRequestLog>
{
    private readonly IAiRequestLogService _aiRequestLogCrudService;
    public GetAiRequestLogQueryHandler(
        IAiRequestLogService aiRequestLogCrudService
        )
    {
        _aiRequestLogCrudService = aiRequestLogCrudService;
    }

    public virtual async Task<AiRequestLog> Handle(GetAiRequestLogQuery request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (string.IsNullOrEmpty(request.LogId))
        {
            throw new ArgumentNullException(nameof(request.LogId));
        }

        var result = await _aiRequestLogCrudService.GetByIdAsync(request.LogId, request.ResponseGroup);

        return result;
    }
}
