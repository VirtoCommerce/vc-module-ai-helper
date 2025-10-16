using System;
using System.Threading;
using System.Threading.Tasks;
using VirtoCommerce.AiHelper.Core.Common;
using VirtoCommerce.AiHelper.Core.Models;
using VirtoCommerce.AiHelper.Core.Services;
using VirtoCommerce.Platform.Core.Settings;
using static VirtoCommerce.AiHelper.Core.ModuleConstants;

namespace VirtoCommerce.AiHelper.Data.Commands.GenerateDescription;
public class GenerateDescriptionCommandHandler : ICommandHandler<GenerateDescriptionCommand, AiRequestResult>
{
    private readonly ISettingsManager _settingsManager;
    private readonly IAiProviderFactory _aiProviderFactory;

    public GenerateDescriptionCommandHandler(
        ISettingsManager settingsManager,
        IAiProviderFactory aiProviderFactory
        )
    {
        _settingsManager = settingsManager;
        _aiProviderFactory = aiProviderFactory;
    }

    public virtual async Task<AiRequestResult> Handle(GenerateDescriptionCommand request, CancellationToken cancellationToken)
    {
        var textGenerationProviderName = await _settingsManager.GetValueAsync<string>(Settings.General.AiHelperTextGenerationProvider);
        var result = new AiRequestResult();

        try
        {
            var textGenerationProvider = _aiProviderFactory.Create(textGenerationProviderName);
            var textGenerationService = textGenerationProvider.GetService<IAiTextGenerationService>();

            var prompt = await textGenerationService.GetProductDescriptionGenerationPrompt();
            prompt = prompt.Replace("{locale}", request.TargetLanguage).Replace("{product}", request.JsonProduct);
            result.Result = await textGenerationService.GenerateTextAsync(prompt);
            result.IsSuccess = true;
        }
        catch (Exception ex)
        {
            result.ErrorMessage = ex.Message;
        }

        return result;

    }
}
