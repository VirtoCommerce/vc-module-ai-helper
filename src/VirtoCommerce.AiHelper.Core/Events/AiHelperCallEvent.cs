using VirtoCommerce.AiHelper.Core.Models;
using VirtoCommerce.Platform.Core.Events;

namespace VirtoCommerce.AiHelper.Core.Events;
public class AiHelperCallEvent : DomainEvent
{
    public AiHelperCallEvent()
    {
    }

    public AiRequestLog AiRequestLog { get; set; }
}
