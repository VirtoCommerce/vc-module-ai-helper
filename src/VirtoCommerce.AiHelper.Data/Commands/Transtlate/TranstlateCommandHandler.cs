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
public class TranstlateCommandHandler : ICommandHandler<TranstlateCommand, AiRequestResult>
{
    private readonly ISettingsManager _settingsManager;
    private readonly IEventPublisher _eventPublisher;
    private readonly IAiProviderFactory _aiProviderFactory;

    public TranstlateCommandHandler(
        ISettingsManager settingsManager,
        IEventPublisher eventPublisher,
        IAiProviderFactory aiProviderFactory
        )
    {
        _settingsManager = settingsManager;
        _eventPublisher = eventPublisher;
        _aiProviderFactory = aiProviderFactory;
    }

    public virtual async Task<AiRequestResult> Handle(TranstlateCommand request, CancellationToken cancellationToken)
    {
        var textGenerationProviderName = await _settingsManager.GetValueAsync<string>(Settings.General.AiHelperTextGenerationProvider);

        var result = new AiRequestResult();
        var callEvent = ExType<AiHelperCallEvent>.New();
        callEvent.AiRequestLog = ExType<AiRequestLog>.New();
        callEvent.AiRequestLog.RequestType = "TextGeneration";
        callEvent.AiRequestLog.TaskType = nameof(TranstlateCommand);
        callEvent.AiRequestLog.ProviderName = textGenerationProviderName;
        //callEvent.AiRequestLog.Model = ???;
        callEvent.AiRequestLog.UserId = request.UserId;
        callEvent.AiRequestLog.EntityId = request.EntityId;
        callEvent.AiRequestLog.EntityType = request.EntityType;
        callEvent.AiRequestLog.RequestContext = JsonConvert.SerializeObject(request);
        var callStartTime = DateTime.UtcNow;

        try
        {
            var textGenerationProvider = _aiProviderFactory.Create(textGenerationProviderName);
            var textGenerationService = textGenerationProvider.GetService<IAiTextGenerationService>();

            var prompt = await textGenerationService.GetTranslationPrompt();
            prompt = prompt.Replace("{locale}", request.TargetLanguage).Replace("{text}", request.Text);

            callEvent.AiRequestLog.Prompt = prompt;

            result = await textGenerationService.GenerateTextAsync(prompt);
        }
        catch (Exception ex)
        {
            result.ErrorMessage = ex.Message;
        }

        callEvent.AiRequestLog.Response = result.Result?.ToString();
        callEvent.AiRequestLog.IsSuccess = result.IsSuccess;
        callEvent.AiRequestLog.ErrorText = result.ErrorMessage;
        callEvent.AiRequestLog.RequestDuration = Convert.ToInt32((DateTime.UtcNow - callStartTime).TotalMilliseconds);
        await _eventPublisher.Publish(callEvent);

        return result;
    }
}
