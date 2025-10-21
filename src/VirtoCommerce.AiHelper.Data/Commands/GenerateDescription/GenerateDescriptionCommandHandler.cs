using System;
using System.Linq;
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
        var result = new AiRequestResult();

        var product = request.Product;
        if (product != null)
        {
            try
            {
                if (product.Images == null || !product.Images.Any())
                {
                    var textGenerationProviderName = await _settingsManager.GetValueAsync<string>(Settings.General.AiHelperTextGenerationProvider);
                    var textGenerationProvider = _aiProviderFactory.Create(textGenerationProviderName);
                    var textGenerationService = textGenerationProvider.GetService<IAiTextGenerationService>();

                    var prompt = await textGenerationService.GetProductDescriptionGenerationPrompt();
                    prompt = prompt.Replace("{locale}", request.TargetLanguage).Replace("{product}", product.ToString());
                    result.Result = await textGenerationService.GenerateTextAsync(prompt);
                    result.IsSuccess = true;
                }
                else
                {
                    var imageRecognitionProviderName = await _settingsManager.GetValueAsync<string>(Settings.General.AiHelperImageRecognitionProvider);
                    var imageRecognitionProvider = _aiProviderFactory.Create(imageRecognitionProviderName);
                    var imageRecognitionService = imageRecognitionProvider.GetService<IAiImageRecognitionService>();

                    var prompt = await imageRecognitionService.GetRecognitionPrompt();
                    prompt = prompt.Replace("{locale}", request.TargetLanguage).Replace("{product.name}", product.Name);

                    result.Result = await imageRecognitionService.RecognizeImageAsync(prompt, product.Images);
                    result.IsSuccess = true;
                }

            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

        }
        else
        {
            result.ErrorMessage = "Product data is missing.";
        }

        return result;
    }
}
