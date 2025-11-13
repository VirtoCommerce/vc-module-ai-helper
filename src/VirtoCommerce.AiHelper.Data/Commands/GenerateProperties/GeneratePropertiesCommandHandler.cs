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

namespace VirtoCommerce.AiHelper.Data.Commands;
public class GeneratePropertiesCommandHandler : ICommandHandler<GeneratePropertiesCommand, AiRequestResult>
{
    private readonly ISettingsManager _settingsManager;
    private readonly IEventPublisher _eventPublisher;
    private readonly IAiProviderFactory _aiProviderFactory;

    public GeneratePropertiesCommandHandler(
        ISettingsManager settingsManager,
        IEventPublisher eventPublisher,
        IAiProviderFactory aiProviderFactory
        )
    {
        _settingsManager = settingsManager;
        _eventPublisher = eventPublisher;
        _aiProviderFactory = aiProviderFactory;
    }

    public virtual async Task<AiRequestResult> Handle(GeneratePropertiesCommand request, CancellationToken cancellationToken)
    {
        var imageRecognitionProviderName = await _settingsManager.GetValueAsync<string>(Settings.General.AiHelperImageRecognitionProvider);

        var result = new AiRequestResult();
        var callEvent = ExType<AiHelperCallEvent>.New();
        callEvent.AiRequestLog = ExType<AiRequestLog>.New();
        callEvent.AiRequestLog.RequestType = "ImageRecognition";
        callEvent.AiRequestLog.TaskType = nameof(GeneratePropertiesCommand);
        callEvent.AiRequestLog.ProviderName = imageRecognitionProviderName;
        //callEvent.AiRequestLog.Model = ???;
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
                if (product.Images != null && product.Images.Any())
                {
                    var imageRecognitionProvider = _aiProviderFactory.Create(imageRecognitionProviderName);
                    var imageRecognitionService = imageRecognitionProvider.GetService<IAiImageRecognitionService>();

                    var prompt = await imageRecognitionService.GetFillPropertiesPrompt();
                    prompt = prompt.Replace("{product.name}", product.Name).Replace("{jsonTemplate}", request.JsonTemplate);

                    callEvent.AiRequestLog.Prompt = prompt;

                    result = await imageRecognitionService.RecognizeImageAsync(prompt, product.Images);
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

        callEvent.AiRequestLog.Response = result.Result?.ToString();
        callEvent.AiRequestLog.IsSuccess = result.IsSuccess;
        callEvent.AiRequestLog.ErrorText = result.ErrorMessage;
        callEvent.AiRequestLog.RequestDuration = Convert.ToInt32((DateTime.UtcNow - callStartTime).TotalMilliseconds);
        await _eventPublisher.Publish(callEvent);

        return result;
    }
}
