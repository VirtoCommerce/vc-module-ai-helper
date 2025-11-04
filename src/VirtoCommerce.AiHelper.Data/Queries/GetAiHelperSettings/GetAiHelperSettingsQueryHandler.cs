using System.Threading;
using System.Threading.Tasks;
using VirtoCommerce.AiHelper.Core.Common;
using VirtoCommerce.AiHelper.Core.Models;
using VirtoCommerce.Platform.Core.Settings;
using static VirtoCommerce.AiHelper.Core.ModuleConstants;

namespace VirtoCommerce.AiHelper.Data.Queries;
public class GetAiHelperSettingsQueryHandler : IQueryHandler<GetAiHelperSettingsQuery, AiHelperSettings>
{
    private readonly ISettingsManager _settingsManager;

    public GetAiHelperSettingsQueryHandler(
        ISettingsManager settingsManager
        )
    {
        _settingsManager = settingsManager;
    }

    public virtual async Task<AiHelperSettings> Handle(GetAiHelperSettingsQuery request, CancellationToken cancellationToken)
    {
        var isEnabled = await _settingsManager.GetValueAsync<bool>(Settings.General.AiHelperEnabled);
        var logLevel = await _settingsManager.GetValueAsync<string>(Settings.General.AiHelperLogLevel);

        var result = ExType<AiHelperSettings>.New();
        result.IsEnabled = isEnabled;
        result.LogLevel = logLevel;

        return result;
    }
}
