using System;
using System.Threading;
using System.Threading.Tasks;
using VirtoCommerce.AiHelper.Core.Common;
using VirtoCommerce.AiHelper.Core.Models.Search;
using VirtoCommerce.AiHelper.Core.Services;

namespace VirtoCommerce.AiHelper.Data.Queries.SearchAiRequestLog;
public class SearchAiRequestLogQueryHandler : IQueryHandler<SearchAiRequestLogQuery, AiRequestLogSearchResult>
{
    private readonly IAiRequestLogSearchService _aiRequestLogSearchServiceSearchService;

    public SearchAiRequestLogQueryHandler(IAiRequestLogSearchService aiRequestLogSearchServiceSearchService)
    {
        _aiRequestLogSearchServiceSearchService = aiRequestLogSearchServiceSearchService;
    }

    public virtual async Task<AiRequestLogSearchResult> Handle(SearchAiRequestLogQuery request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var searchCriteria = request.ToCriteria();
        var result = await _aiRequestLogSearchServiceSearchService.SearchAsync(searchCriteria, false);
        return result;
    }
}
