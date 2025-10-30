using System.Threading.Tasks;
using VirtoCommerce.AiHelper.Core.Models;
using static VirtoCommerce.AiHelper.Core.ModuleConstants;

namespace VirtoCommerce.AiHelper.Core.Services;
public interface IAiImageGenerationService : IAiTask
{
    Task<AiRequestResult> GenerateImageAsync(string prompt, int width = 0, int height = 0);

    Task<string> GetProductImageGenerationPrompt() => Task.FromResult(DefaultPrompts.ProductImageGeneration);
}
