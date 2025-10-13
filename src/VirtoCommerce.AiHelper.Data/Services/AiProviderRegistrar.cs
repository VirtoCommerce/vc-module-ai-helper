using System;
using System.Collections.Generic;
using System.Linq;
using VirtoCommerce.AiHelper.Core.Services;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.AiHelper.Data.Services;
public class AiProviderRegistrar : IAiProviderRegistrar, IAiProviderFactory
{
    private readonly IServiceProvider _serviceProvider;

    public AiProviderRegistrar(
        IServiceProvider serviceProvider
        )
    {
        _serviceProvider = serviceProvider;
    }

    public IAiProvider Create(string providerType)
    {
        return AbstractTypeFactory<IAiProvider>.TryCreateInstance(providerType);
    }

    public AiProviderBuilder Register<TAiProvider>(Func<IAiProvider> factory = null) where TAiProvider : IAiProvider
    {
        var typeInfo = AbstractTypeFactory<IAiProvider>.RegisterType<TAiProvider>();
        var builder = new AiProviderBuilder(_serviceProvider, typeof(TAiProvider), factory);
        typeInfo.WithFactory(() => builder.Build());
        return builder;
    }

    IEnumerable<IAiProvider> IAiProviderRegistrar.GetAllAiProviders()
    {
        return AbstractTypeFactory<IAiProvider>.AllTypeInfos.Select(x => AbstractTypeFactory<IAiProvider>.TryCreateInstance(x.TypeName));
    }

    IEnumerable<IAiProvider> IAiProviderRegistrar.GetAiProvidersByService<TAiService>()
    {
        return ((IAiProviderRegistrar)this).GetAllAiProviders().Where(x => x.AvailableServices?.OfType<TAiService>()?.Any() ?? false);
    }
}
