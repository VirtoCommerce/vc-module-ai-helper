using System.Linq;
using System.Threading.Tasks;
using VirtoCommerce.AiHelper.Data.Models;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.AiHelper.Data.Repositories;
public interface IAiHelperRepository : IRepository
{
    IQueryable<AiRequestLogEntity> AiRequestLogs { get; }

    Task<AiRequestLogEntity[]> GetAiRequestLogsByIds(string[] ids, string responseGroup = null);

}
