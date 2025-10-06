using System.Threading.Tasks;

namespace VirtoCommerce.AiHelper.Core.Services;
public interface IAiTranslationService
{
    Task<string> TranslateAsync(string text, string targetLanguage, string sourceLanguage = null);
}
