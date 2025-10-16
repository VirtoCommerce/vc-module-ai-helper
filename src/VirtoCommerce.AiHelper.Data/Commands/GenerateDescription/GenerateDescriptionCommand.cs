using VirtoCommerce.AiHelper.Core.Common;
using VirtoCommerce.AiHelper.Core.Models;

namespace VirtoCommerce.AiHelper.Data.Commands;
public class GenerateDescriptionCommand : ICommand<AiRequestResult>
{
    public string JsonProduct { get; set; }
    public string TargetLanguage { get; set; }
}
