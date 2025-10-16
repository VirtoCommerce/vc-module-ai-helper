using System.Threading.Tasks;
using VirtoCommerce.AiHelper.Core.Models;
using static VirtoCommerce.AiHelper.Core.ModuleConstants;

namespace VirtoCommerce.AiHelper.Core.Services;
public interface IAiTextGenerationService : IAiTask
{
    Task<AiRequestResult> GenerateTextAsync(string prompt, string context = null);

    Task<string> GetTranslationPrompt() => Task.FromResult(DefaultPrompts.Translation);
    Task<string> GetProductDescriptionGenerationPrompt() => Task.FromResult(DefaultPrompts.ProductDescriptionGeneration);
}
