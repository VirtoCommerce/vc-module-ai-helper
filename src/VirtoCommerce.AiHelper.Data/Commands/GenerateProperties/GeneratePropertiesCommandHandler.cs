using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VirtoCommerce.AiHelper.Core.Common;
using VirtoCommerce.AiHelper.Core.Models;
using VirtoCommerce.AiHelper.Core.Services;
using VirtoCommerce.Platform.Core.Settings;
using static VirtoCommerce.AiHelper.Core.ModuleConstants;

namespace VirtoCommerce.AiHelper.Data.Commands;
public class GeneratePropertiesCommandHandler : ICommandHandler<GeneratePropertiesCommand, AiRequestResult>
{
    private readonly ISettingsManager _settingsManager;
    private readonly IAiProviderFactory _aiProviderFactory;

    public GeneratePropertiesCommandHandler(
        ISettingsManager settingsManager,
        IAiProviderFactory aiProviderFactory
        )
    {
        _settingsManager = settingsManager;
        _aiProviderFactory = aiProviderFactory;
    }

    public virtual async Task<AiRequestResult> Handle(GeneratePropertiesCommand request, CancellationToken cancellationToken)
    {
        var result = new AiRequestResult();

        var product = request.Product;
        if (product != null)
        {
            try
            {
                if (product.Images != null && product.Images.Any())
                {
                    var imageRecognitionProviderName = await _settingsManager.GetValueAsync<string>(Settings.General.AiHelperImageRecognitionProvider);
                    var imageRecognitionProvider = _aiProviderFactory.Create(imageRecognitionProviderName);
                    var imageRecognitionService = imageRecognitionProvider.GetService<IAiImageRecognitionService>();

                    var prompt = await imageRecognitionService.GetFillPropertiesPrompt();
                    prompt = prompt.Replace("{product.name}", product.Name).Replace("{jsonTemplate}", request.JsonTemplate);

                    result.Result = await imageRecognitionService.RecognizeImageAsync(prompt, product.Images);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ErrorMessage = "Product images are missing.";
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
