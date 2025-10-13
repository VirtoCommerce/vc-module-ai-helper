using System.Threading;
using System.Threading.Tasks;
using VirtoCommerce.AiHelper.Core.Common;
using VirtoCommerce.AiHelper.Core.Models;
using VirtoCommerce.AiHelper.Core.Services;
using VirtoCommerce.Platform.Core.Settings;
using static VirtoCommerce.AiHelper.Core.ModuleConstants;

namespace VirtoCommerce.AiHelper.Data.Commands;
public class TranstlateCommandHandler : ICommandHandler<TranstlateCommand, AiRequestResult>
{
    private readonly ISettingsManager _settingsManager;
    private readonly IAiProviderFactory _aiProviderFactory;

    public TranstlateCommandHandler(
        ISettingsManager settingsManager,
        IAiProviderFactory aiProviderFactory
        )
    {
        _settingsManager = settingsManager;
        _aiProviderFactory = aiProviderFactory;
    }

    public virtual async Task<AiRequestResult> Handle(TranstlateCommand request, CancellationToken cancellationToken)
    {
        var translationProvider = await _settingsManager.GetValueAsync<string>(Settings.General.AiHelperTranslationProvider);
        var result = new AiRequestResult();

        try
        {
            var provider = _aiProviderFactory.Create(translationProvider);
            result.Result = await provider.GetService<IAiTranslationService>().TranslateAsync(request.Text, request.TargetLanguage);
            result.IsSuccess = true;
        }
        catch (System.Exception ex)
        {
            result.ErrorMessage = ex.Message;
        }

        return result;
    }
}
