using System;
using System.Collections.Generic;

namespace VirtoCommerce.AiHelper.Core.Services;
public class AiProviderBuilder
{
    public AiProviderBuilder(IServiceProvider serviceProvider, Type providerType, Func<IAiProvider> factory = null)
    {
        ServiceProvider = serviceProvider;
        AiProvider = factory != null ? factory() : Activator.CreateInstance(providerType) as IAiProvider;
    }

    public IServiceProvider ServiceProvider { get; }
    public IAiProvider AiProvider { get; }

    public AiProviderBuilder WithService(IAiTask aiService)
    {
        if (AiProvider.AvailableServices == null)
        {
            AiProvider.AvailableServices = new List<IAiTask>();
        }
        AiProvider.AvailableServices.Add(aiService);

        return this;
    }

    public IAiProvider Build()
    {
        return AiProvider.Clone() as IAiProvider;
    }
}
