using System.Threading.Tasks;
using OpenAI.Responses;
using VirtoCommerce.AiHelper.Core.Services;
using VirtoCommerce.Platform.Core.Settings;
using static VirtoCommerce.AiHelper.Core.ModuleConstants;

namespace VirtoCommerce.AiHelper.Data.Services;
public class AiTranslationService : IAiTranslationService
{
    private readonly ISettingsManager _settingsManager;
    public AiTranslationService(ISettingsManager settingsManager)
    {
        _settingsManager = settingsManager;
    }

    public virtual async Task<string> TranslateAsync(string text, string targetLanguage, string sourceLanguage = null)
    {
        var result = string.Empty;

        var apiKey = await _settingsManager.GetValueAsync<string>(Settings.General.AiHelperOpenAiKey);
        var model = await _settingsManager.GetValueAsync<string>(Settings.General.AiHelperOpenAiModel);
        var prompt = await _settingsManager.GetValueAsync<string>(Settings.General.AiHelperOpenAiPromptTranslate);

        if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(model) && !string.IsNullOrEmpty(prompt))
        {
            prompt = prompt.Replace("{locale}", targetLanguage).Replace("{text}", text);

#pragma warning disable OPENAI001
            var client = new OpenAIResponseClient(model: model, apiKey: apiKey);
            OpenAIResponse response = client.CreateResponse(prompt);
#pragma warning restore OPENAI001

            result = response.GetOutputText();
        }
        return result;
    }
}
