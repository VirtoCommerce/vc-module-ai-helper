using System.Threading.Tasks;

namespace VirtoCommerce.AiHelper.Core.Services;
public interface IAiTranslationService : IAiTask
{
    Task<string> TranslateAsync(string text, string targetLanguage, string sourceLanguage = null);
}
