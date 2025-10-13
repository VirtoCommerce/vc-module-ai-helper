using VirtoCommerce.AiHelper.Core.Services;

namespace VirtoCommerce.AiHelper.Data.Services;
public class DummyAiProvider : AbstractAiProvider
{
    public override string ProviderName => "DummyAiProvider";
    public override string ProviderType => nameof(DummyAiProvider);
}
