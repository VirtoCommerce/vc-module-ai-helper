using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.AiHelper.Core.Models.Search;
public class AiRequestLogSearchCriteria : SearchCriteriaBase
{
    public string ProviderName { get; set; }
    public string RequestType { get; set; }
    public string TaskType { get; set; }
    public bool? IsSuccess { get; set; }
    public string UserId { get; set; }
    public string EntityId { get; set; }
}
