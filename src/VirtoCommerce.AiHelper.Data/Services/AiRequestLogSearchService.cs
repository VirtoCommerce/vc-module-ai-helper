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

        //if (!string.IsNullOrEmpty(criteria.AttributeKey))
        //{
        //    query = query.Where(x => x.AttributeKey.StartsWith(criteria.AttributeKey));
        //}

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
