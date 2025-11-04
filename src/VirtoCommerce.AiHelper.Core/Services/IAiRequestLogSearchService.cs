using VirtoCommerce.AiHelper.Core.Models;
using VirtoCommerce.AiHelper.Core.Models.Search;
using VirtoCommerce.Platform.Core.GenericCrud;

namespace VirtoCommerce.AiHelper.Core.Services;
public interface IAiRequestLogSearchService : ISearchService<AiRequestLogSearchCriteria, AiRequestLogSearchResult, AiRequestLog>
{
}
