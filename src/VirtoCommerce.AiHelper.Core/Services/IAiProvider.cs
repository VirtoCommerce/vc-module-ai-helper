using System;
using System.Collections.Generic;

namespace VirtoCommerce.AiHelper.Core.Services;
public interface IAiProvider : ICloneable
{
    string ProviderName { get; }
    string ProviderType { get; }
    List<IAiTask> AvailableServices { get; set; }
    IAiTask GetService<IAiTask>();
}
