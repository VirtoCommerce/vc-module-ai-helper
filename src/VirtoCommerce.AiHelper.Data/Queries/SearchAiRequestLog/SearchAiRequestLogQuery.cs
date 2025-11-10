using VirtoCommerce.AiHelper.Core.Common;
using VirtoCommerce.AiHelper.Core.Models.Search;

namespace VirtoCommerce.AiHelper.Data.Queries;
public class SearchAiRequestLogQuery : AiRequestLogSearchCriteria, IQuery<AiRequestLogSearchResult>
{
    public virtual AiRequestLogSearchCriteria ToCriteria()
    {
        var criteria = ExType<AiRequestLogSearchCriteria>.New();

        criteria.ProviderName = ProviderName;
        criteria.RequestType = RequestType;
        criteria.TaskType = TaskType;
        criteria.IsSuccess = IsSuccess;
        criteria.UserId = UserId;
        criteria.EntityId = EntityId;

        criteria.Keyword = Keyword;
        criteria.ResponseGroup = ResponseGroup;
        criteria.ObjectIds = ObjectIds;
        criteria.Take = Take;
        criteria.Skip = Skip;
        criteria.SearchPhrase = SearchPhrase;

        return criteria;
    }
}
