using System;
using System.Collections.Generic;

namespace VirtoCommerce.AiHelper.Core.Services;
public interface IAiProviderRegistrar
{
    AiProviderBuilder Register<TAiProvider>(Func<IAiProvider> factory = null) where TAiProvider : IAiProvider;

    IEnumerable<IAiProvider> GetAllAiProviders();
    IEnumerable<IAiProvider> GetAiProvidersByService<TAiService>();
}
