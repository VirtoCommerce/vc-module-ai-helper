using VirtoCommerce.AiHelper.Core.Common;
using VirtoCommerce.AiHelper.Core.Models;

namespace VirtoCommerce.AiHelper.Data.Commands;
public class TranstlateCommand : ICommand<AiRequestResult>, IHasVirtoContext
{
    public string Text { get; set; }
    public string TargetLanguage { get; set; }
    public string UserId { get; set; }
    public string EntityId { get; set; }
}
