using System.Threading.Tasks;

namespace VirtoCommerce.AiHelper.Core.Services;
public interface IAiTextGenerationService : IAiTask
{
    Task<string> GenerateTextAsync(string prompt, string context = null);
}
