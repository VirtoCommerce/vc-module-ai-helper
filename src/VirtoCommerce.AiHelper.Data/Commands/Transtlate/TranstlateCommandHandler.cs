using System.Threading;
using System.Threading.Tasks;
using VirtoCommerce.AiHelper.Core.Common;
using VirtoCommerce.AiHelper.Core.Models;
using VirtoCommerce.AiHelper.Core.Services;

namespace VirtoCommerce.AiHelper.Data.Commands;
public class TranstlateCommandHandler : ICommandHandler<TranstlateCommand, AiRequestResult>
{
    private readonly IAiTranslationService _aiTranslationService;

    public TranstlateCommandHandler(
        IAiTranslationService aiTranslationService
        )
    {
        _aiTranslationService = aiTranslationService;
    }

    public virtual async Task<AiRequestResult> Handle(TranstlateCommand request, CancellationToken cancellationToken)
    {
        var result = new AiRequestResult();

        result.Result = await _aiTranslationService.TranslateAsync(request.Text, request.TargetLanguage);

        return result;
    }
}
