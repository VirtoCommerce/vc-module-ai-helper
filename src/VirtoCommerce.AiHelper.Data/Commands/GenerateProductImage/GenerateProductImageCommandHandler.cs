using System;
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
public class GenerateProductImageCommandHandler : ICommandHandler<GenerateProductImageCommand, AiRequestResult>
{
    private readonly ISettingsManager _settingsManager;
    private readonly IEventPublisher _eventPublisher;
    private readonly IAiProviderFactory _aiProviderFactory;

    public GenerateProductImageCommandHandler(
        ISettingsManager settingsManager,
        IEventPublisher eventPublisher,
        IAiProviderFactory aiProviderFactory
        )
    {
        _settingsManager = settingsManager;
        _eventPublisher = eventPublisher;
        _aiProviderFactory = aiProviderFactory;
    }
    public virtual async Task<AiRequestResult> Handle(GenerateProductImageCommand request, CancellationToken cancellationToken)
    {
        var imageGenerationProviderName = await _settingsManager.GetValueAsync<string>(Settings.General.AiHelperImageGenerationProvider);

        var result = new AiRequestResult();
        var callEvent = ExType<AiHelperCallEvent>.New();
        callEvent.AiRequestLog = ExType<AiRequestLog>.New();
        callEvent.AiRequestLog.RequestType = "ImageGeneration";
        callEvent.AiRequestLog.TaskType = nameof(GenerateProductImageCommand);
        callEvent.AiRequestLog.ProviderName = imageGenerationProviderName;
        //callEvent.AiRequestLog.Model = ???;
        callEvent.AiRequestLog.UserId = request.UserId;
        callEvent.AiRequestLog.EntityId = request.EntityId;
        callEvent.AiRequestLog.EntityType = request.EntityType;
        callEvent.AiRequestLog.RequestContext = JsonConvert.SerializeObject(request);
        var callStartTime = DateTime.UtcNow;

        var product = request.Product;
        if (product != null)
        {
            var imageGenerationProvider = _aiProviderFactory.Create(imageGenerationProviderName);
            var imageGenerationService = imageGenerationProvider.GetService<IAiImageGenerationService>();

            var prompt = await imageGenerationService.GetProductImageGenerationPrompt();
            prompt = prompt.Replace("{product.name}", product.Name).Replace("{product.description}", product.Description);

            callEvent.AiRequestLog.Prompt = prompt;

            result = await imageGenerationService.GenerateImageAsync(prompt);
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
