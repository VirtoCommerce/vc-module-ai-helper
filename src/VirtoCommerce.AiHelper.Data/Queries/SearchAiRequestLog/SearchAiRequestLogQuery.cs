using VirtoCommerce.AiHelper.Core.Common;
using VirtoCommerce.AiHelper.Core.Models.Search;

namespace VirtoCommerce.AiHelper.Data.Queries;
public class SearchAiRequestLogQuery : AiRequestLogSearchCriteria, IQuery<AiRequestLogSearchResult>
{
    public virtual AiRequestLogSearchCriteria ToCriteria()
    {
        var criteria = ExType<AiRequestLogSearchCriteria>.New();

        criteria.ResponseGroup = ResponseGroup;
        criteria.ObjectIds = ObjectIds;
        criteria.Take = Take;
        criteria.Skip = Skip;
        criteria.SearchPhrase = SearchPhrase;

        return criteria;
    }
}
