using System.Threading;
using System.Threading.Tasks;
using VirtoCommerce.AiHelper.Core.Common;
using VirtoCommerce.AiHelper.Core.Models;
using VirtoCommerce.AiHelper.Core.Services;
using VirtoCommerce.Platform.Core.Settings;
using static VirtoCommerce.AiHelper.Core.ModuleConstants;

namespace VirtoCommerce.AiHelper.Data.Commands;
public class GenerateProductImageCommandHandler : ICommandHandler<GenerateProductImageCommand, AiRequestResult>
{
    private readonly ISettingsManager _settingsManager;
    private readonly IAiProviderFactory _aiProviderFactory;

    public GenerateProductImageCommandHandler(
        ISettingsManager settingsManager,
        IAiProviderFactory aiProviderFactory
        )
    {
        _settingsManager = settingsManager;
        _aiProviderFactory = aiProviderFactory;
    }
    public virtual async Task<AiRequestResult> Handle(GenerateProductImageCommand request, CancellationToken cancellationToken)
    {
        var result = new AiRequestResult();

        var product = request.Product;
        if (product != null)
        {
            var imageGenerationProviderName = await _settingsManager.GetValueAsync<string>(Settings.General.AiHelperImageGenerationProvider);
            var imageGenerationProvider = _aiProviderFactory.Create(imageGenerationProviderName);
            var imageGenerationService = imageGenerationProvider.GetService<IAiImageGenerationService>();

            var prompt = await imageGenerationService.GetProductImageGenerationPrompt();
            prompt = prompt.Replace("{product.name}", product.Name).Replace("{product.description}", product.Description);

            result.Result = await imageGenerationService.GenerateImageAsync(prompt);
            result.IsSuccess = true;

        }
        else
        {
            result.ErrorMessage = "Product data is missing.";
        }

        return result;
    }
}
