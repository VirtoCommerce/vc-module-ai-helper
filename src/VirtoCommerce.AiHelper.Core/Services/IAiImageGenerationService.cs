using System.Threading.Tasks;
using VirtoCommerce.AiHelper.Core.Models;

namespace VirtoCommerce.AiHelper.Core.Services;
public interface IAiImageGenerationService : IAiTask
{
    Task<AiRequestResult> GenerateImageAsync(string prompt, int width = 0, int height = 0);
}
