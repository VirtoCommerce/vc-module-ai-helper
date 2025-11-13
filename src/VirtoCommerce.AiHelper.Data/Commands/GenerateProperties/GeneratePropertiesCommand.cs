using VirtoCommerce.AiHelper.Core.Common;
using VirtoCommerce.AiHelper.Core.Models;

namespace VirtoCommerce.AiHelper.Data.Commands;
public class GeneratePropertiesCommand : ICommand<AiRequestResult>, IHasVirtoContext
{
    public AiProductContract Product { get; set; }
    public string JsonTemplate { get; set; }
    public string UserId { get; set; }
    public string EntityId { get; set; }
    public string EntityType { get; set; }
}
