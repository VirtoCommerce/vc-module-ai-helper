using System.Threading.Tasks;
using VirtoCommerce.AiHelper.Core.Models;
using static VirtoCommerce.AiHelper.Core.ModuleConstants;

namespace VirtoCommerce.AiHelper.Core.Services;
public interface IAiImageRecognitionService : IAiTask
{
    Task<AiRequestResult> RecognizeImageAsync(string prompt, string[] images, string context = null);
    Task<string> GetRecognitionPrompt() => Task.FromResult(DefaultPrompts.ImageRecognition);
}
