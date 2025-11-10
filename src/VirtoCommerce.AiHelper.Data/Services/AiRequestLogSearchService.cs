using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using VirtoCommerce.AiHelper.Core.Models;
using VirtoCommerce.AiHelper.Core.Models.Search;
using VirtoCommerce.AiHelper.Core.Services;
using VirtoCommerce.AiHelper.Data.Models;
using VirtoCommerce.AiHelper.Data.Repositories;
using VirtoCommerce.Platform.Core.Caching;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.GenericCrud;
using VirtoCommerce.Platform.Data.GenericCrud;

namespace VirtoCommerce.AiHelper.Data.Services;
public class AiRequestLogSearchService : SearchService<AiRequestLogSearchCriteria, AiRequestLogSearchResult, AiRequestLog, AiRequestLogEntity>,
    IAiRequestLogSearchService
{
    public AiRequestLogSearchService(
        Func<IAiHelperRepository> repositoryFactory,
        IPlatformMemoryCache platformMemoryCache,
        IAiRequestLogService crudService,
        IOptions<CrudOptions> crudOptions
    )
        : base(repositoryFactory, platformMemoryCache, crudService, crudOptions)
    {
    }

    protected override IQueryable<AiRequestLogEntity> BuildQuery(IRepository repository, AiRequestLogSearchCriteria criteria)
    {
        var query = ((IAiHelperRepository)repository).AiRequestLogs;

        if (!string.IsNullOrEmpty(criteria.ProviderName))
        {
            query = query.Where(x => x.ProviderName == criteria.ProviderName);
        }
        if (!string.IsNullOrEmpty(criteria.RequestType))
        {
            query = query.Where(x => x.RequestType == criteria.RequestType);
        }
        if (!string.IsNullOrEmpty(criteria.TaskType))
        {
            query = query.Where(x => x.TaskType == criteria.TaskType);
        }
        if (criteria.IsSuccess.HasValue)
        {
            query = query.Where(x => x.IsSuccess == criteria.IsSuccess.Value);
        }
        if (!string.IsNullOrEmpty(criteria.UserId))
        {
            query = query.Where(x => x.UserId == criteria.UserId);
        }
        if (!string.IsNullOrEmpty(criteria.EntityId))
        {
            query = query.Where(x => x.EntityId == criteria.EntityId);
        }
        if (!string.IsNullOrEmpty(criteria.Keyword))
        {
            query = query.Where(x => x.ProviderName.Contains(criteria.Keyword) ||
                x.RequestType.Contains(criteria.Keyword) ||
                x.TaskType.Contains(criteria.Keyword) ||
                x.UserId.Contains(criteria.Keyword) ||
                x.EntityId.Contains(criteria.Keyword) ||
                x.Prompt.Contains(criteria.Keyword) ||
                x.Response.Contains(criteria.Keyword) ||
                x.ErrorText.Contains(criteria.Keyword));
        }

        return query;
    }

    protected override IList<SortInfo> BuildSortExpression(AiRequestLogSearchCriteria criteria)
    {
        var sortInfos = criteria.SortInfos;
        if (sortInfos.IsNullOrEmpty())
        {
            sortInfos =
                [
                    new SortInfo
                    {
                        SortColumn = ReflectionUtility.GetPropertyName<AiRequestLogEntity>(x => x.CreatedDate),
                        SortDirection = SortDirection.Descending
                    }
                ];
        }

        return sortInfos;
    }

    protected override Task<AiRequestLogSearchResult> ProcessSearchResultAsync(AiRequestLogSearchResult result, AiRequestLogSearchCriteria criteria)
    {
        var respGroupEnum = EnumUtility.SafeParseFlags(criteria.ResponseGroup, AiRequestLogResponseGroup.None);

        if (!result.Results.IsNullOrEmpty() && !respGroupEnum.HasFlag(AiRequestLogResponseGroup.Verbose))
        {
            foreach (var logItem in result.Results)
            {
                logItem.RequestContext = null;
                logItem.Prompt = null;
                logItem.Response = null;
                logItem.ErrorText = null;
            }
        }

        return Task.FromResult(result);
    }

}
