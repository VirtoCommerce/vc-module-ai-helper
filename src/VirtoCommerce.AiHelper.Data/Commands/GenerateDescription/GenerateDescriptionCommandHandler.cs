using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VirtoCommerce.AiHelper.Core.Common;
using VirtoCommerce.AiHelper.Core.Events;
using VirtoCommerce.AiHelper.Core.Models;
using VirtoCommerce.AiHelper.Core.Services;
using VirtoCommerce.Platform.Core.Events;
using VirtoCommerce.Platform.Core.Settings;
using static VirtoCommerce.AiHelper.Core.ModuleConstants;

namespace VirtoCommerce.AiHelper.Data.Commands.GenerateDescription;
public class GenerateDescriptionCommandHandler : ICommandHandler<GenerateDescriptionCommand, AiRequestResult>
{
    private readonly ISettingsManager _settingsManager;
    private readonly IEventPublisher _eventPublisher;
    private readonly IAiProviderFactory _aiProviderFactory;

    public GenerateDescriptionCommandHandler(
        ISettingsManager settingsManager,
        IEventPublisher eventPublisher,
        IAiProviderFactory aiProviderFactory
        )
    {
        _settingsManager = settingsManager;
        _eventPublisher = eventPublisher;
        _aiProviderFactory = aiProviderFactory;
    }

    public virtual async Task<AiRequestResult> Handle(GenerateDescriptionCommand request, CancellationToken cancellationToken)
    {
        var textGenerationProviderName = await _settingsManager.GetValueAsync<string>(Settings.General.AiHelperTextGenerationProvider);
        var imageRecognitionProviderName = await _settingsManager.GetValueAsync<string>(Settings.General.AiHelperImageRecognitionProvider);

        var result = new AiRequestResult();
        var callEvent = ExType<AiHelperCallEvent>.New();
        callEvent.AiRequestLog = ExType<AiRequestLog>.New();
        callEvent.AiRequestLog.TaskType = nameof(GenerateDescriptionCommand);
        callEvent.AiRequestLog.UserId = request.UserId;
        callEvent.AiRequestLog.EntityId = request.EntityId;
        callEvent.AiRequestLog.EntityType = request.EntityType;
        callEvent.AiRequestLog.RequestContext = JsonConvert.SerializeObject(request);
        var callStartTime = DateTime.UtcNow;

        var product = request.Product;
        if (product != null)
        {
            try
            {
                if (product.Images == null || !product.Images.Any())
                {
                    callEvent.AiRequestLog.ProviderName = textGenerationProviderName;
                    //callEvent.AiRequestLog.Model = ???;
                    callEvent.AiRequestLog.RequestType = "TextGeneration";

                    var textGenerationProvider = _aiProviderFactory.Create(textGenerationProviderName);
                    var textGenerationService = textGenerationProvider.GetService<IAiTextGenerationService>();

                    var prompt = await textGenerationService.GetProductDescriptionGenerationPrompt();
                    prompt = prompt.Replace("{locale}", request.TargetLanguage).Replace("{product}", product.ToString());

                    callEvent.AiRequestLog.Prompt = prompt;

                    result = await textGenerationService.GenerateTextAsync(prompt);
                }
                else
                {
                    callEvent.AiRequestLog.ProviderName = imageRecognitionProviderName;
                    //callEvent.AiRequestLog.Model = ???;
                    callEvent.AiRequestLog.RequestType = "ImageRecognition";

                    var imageRecognitionProvider = _aiProviderFactory.Create(imageRecognitionProviderName);
                    var imageRecognitionService = imageRecognitionProvider.GetService<IAiImageRecognitionService>();

                    var prompt = await imageRecognitionService.GetRecognitionPrompt();
                    prompt = prompt.Replace("{locale}", request.TargetLanguage).Replace("{product.name}", product.Name);

                    callEvent.AiRequestLog.Prompt = prompt;

                    result = await imageRecognitionService.RecognizeImageAsync(prompt, product.Images);
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

        callEvent.AiRequestLog.Response = result.Result?.ToString();
        callEvent.AiRequestLog.IsSuccess = result.IsSuccess;
        callEvent.AiRequestLog.ErrorText = result.ErrorMessage;
        callEvent.AiRequestLog.RequestDuration = Convert.ToInt32((DateTime.UtcNow - callStartTime).TotalMilliseconds);
        await _eventPublisher.Publish(callEvent);

        return result;
    }
}
