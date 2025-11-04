using System.Threading.Tasks;
using VirtoCommerce.AiHelper.Core.Models;
using VirtoCommerce.Platform.Core.GenericCrud;

namespace VirtoCommerce.AiHelper.Core.Services;
public interface IAiRequestLogService : ICrudService<AiRequestLog>
{
    Task<AiRequestLog> SaveRequestLog(AiRequestLog aiRequestLog);
}
