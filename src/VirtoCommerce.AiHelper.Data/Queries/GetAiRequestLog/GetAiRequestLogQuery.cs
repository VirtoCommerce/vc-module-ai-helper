using System.ComponentModel.DataAnnotations;
using VirtoCommerce.AiHelper.Core.Common;
using VirtoCommerce.AiHelper.Core.Models;

namespace VirtoCommerce.AiHelper.Data.Queries;
public class GetAiRequestLogQuery : IQuery<AiRequestLog>
{
    [Required]
    public string LogId { get; set; }
    public string ResponseGroup { get; set; }
}
