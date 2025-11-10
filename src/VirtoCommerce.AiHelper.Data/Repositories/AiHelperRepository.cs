using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirtoCommerce.AiHelper.Core.Models;
using VirtoCommerce.AiHelper.Data.Models;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Data.Infrastructure;

namespace VirtoCommerce.AiHelper.Data.Repositories;
public class AiHelperRepository : DbContextRepositoryBase<AiHelperDbContext>, IAiHelperRepository
{
    public AiHelperRepository(AiHelperDbContext dbContext)
        : base(dbContext)
    {
    }

    public IQueryable<AiRequestLogEntity> AiRequestLogs => DbContext.Set<AiRequestLogEntity>();

    public virtual async Task<AiRequestLogEntity[]> GetAiRequestLogsByIds(string[] ids, string responseGroup = null)
    {
        var result = Array.Empty<AiRequestLogEntity>();

        if (!ids.IsNullOrEmpty())
        {
            result = await AiRequestLogs.
                Where(x => ids.Contains(x.Id)).ToArrayAsync();
        }

        var respGroupEnum = EnumUtility.SafeParseFlags(responseGroup, AiRequestLogResponseGroup.None);

        if (!result.IsNullOrEmpty() && !respGroupEnum.HasFlag(AiRequestLogResponseGroup.Verbose))
        {
            foreach (var logItem in result)
            {
                logItem.RequestContext = null;
                logItem.Prompt = null;
                logItem.Response = null;
                logItem.ErrorText = null;
            }
        }

        return result;
    }

}
