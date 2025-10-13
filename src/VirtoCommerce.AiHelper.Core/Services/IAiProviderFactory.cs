namespace VirtoCommerce.AiHelper.Core.Services;
public interface IAiProviderFactory
{
    IAiProvider Create(string providerType);
}
