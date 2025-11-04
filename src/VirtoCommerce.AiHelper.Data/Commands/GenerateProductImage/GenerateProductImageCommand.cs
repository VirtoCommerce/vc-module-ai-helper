using VirtoCommerce.AiHelper.Core.Common;
using VirtoCommerce.AiHelper.Core.Models;

namespace VirtoCommerce.AiHelper.Data.Commands;
public class GenerateProductImageCommand : ICommand<AiRequestResult>, IHasVirtoContext
{
    public AiProductContract Product { get; set; }
    public string UserId { get; set; }
    public string EntityId { get; set; }
}
