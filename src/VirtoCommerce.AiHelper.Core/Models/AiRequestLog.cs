using System;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.AiHelper.Core.Models;
public class AiRequestLog : AuditableEntity, ICloneable
{
    public string ProviderName { get; set; }
    public string Model { get; set; }
    public string RequestType { get; set; }
    public string TaskType { get; set; }

    public string UserId { get; set; }
    public string EntityId { get; set; }

    public string RequestContext { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }

    public object Clone()
    {
        var result = MemberwiseClone() as AiRequestLog;
        return result;
    }
}
