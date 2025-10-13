using System.Collections.Generic;

namespace VirtoCommerce.AiHelper.Core.Services;
public abstract class AbstractAiProvider : IAiProvider
{
    private List<IAiTask> _availableServices;

    public virtual List<IAiTask> AvailableServices
    {
        get => _availableServices;
        set => _availableServices = value;
    }

    public abstract string ProviderName { get; }
    public abstract string ProviderType { get; }

    public virtual IAiTask GetService<IAiTask>()
    {
        var result = default(IAiTask);

        foreach (var service in _availableServices)
        {
            if (service is IAiTask)
            {
                result = (IAiTask)service;
                break;
            }
        }

        return result;
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}
