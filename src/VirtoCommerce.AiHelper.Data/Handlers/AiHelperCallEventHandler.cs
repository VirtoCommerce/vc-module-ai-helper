using System.Threading.Tasks;
using VirtoCommerce.AiHelper.Core.Events;
using VirtoCommerce.AiHelper.Core.Models;
using VirtoCommerce.AiHelper.Core.Services;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Events;
using VirtoCommerce.Platform.Core.Settings;
using static VirtoCommerce.AiHelper.Core.ModuleConstants;

namespace VirtoCommerce.AiHelper.Data.Handlers;
public class AiHelperCallEventHandler : IEventHandler<AiHelperCallEvent>
{
    private readonly IAiRequestLogService _aiRequestLogService;
    private readonly ISettingsManager _settingsManager;

    public AiHelperCallEventHandler(
        IAiRequestLogService aiRequestLogService,
        ISettingsManager settingsManager
        )
    {
        _aiRequestLogService = aiRequestLogService;
        _settingsManager = settingsManager;
    }

    public virtual async Task Handle(AiHelperCallEvent message)
    {
        if (message != null && message.AiRequestLog != null)
        {
            var logLevel = await _settingsManager.GetValueAsync<string>(Settings.General.AiHelperLogLevel);
            var respGroupEnum = EnumUtility.SafeParseFlags(logLevel, AiRequestLogResponseGroup.None);

            if (respGroupEnum != AiRequestLogResponseGroup.None)
            {
                var aiRequestLog = message.AiRequestLog;

                if (!respGroupEnum.HasFlag(AiRequestLogResponseGroup.Verbose))
                {
                    aiRequestLog.RequestContext = null;
                    aiRequestLog.Prompt = null;
                    aiRequestLog.Response = null;
                    aiRequestLog.ErrorText = null;
                }

                await _aiRequestLogService.SaveRequestLog(aiRequestLog);
            }
        }
    }
}
