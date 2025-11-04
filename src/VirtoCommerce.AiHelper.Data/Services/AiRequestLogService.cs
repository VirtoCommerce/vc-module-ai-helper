using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtoCommerce.AiHelper.Core.Models;
using VirtoCommerce.AiHelper.Core.Services;
using VirtoCommerce.AiHelper.Data.Models;
using VirtoCommerce.AiHelper.Data.Repositories;
using VirtoCommerce.Platform.Core.Caching;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Events;
using VirtoCommerce.Platform.Data.GenericCrud;

namespace VirtoCommerce.AiHelper.Data.Services;
public class AiRequestLogService : CrudService<AiRequestLog, AiRequestLogEntity,
    GenericChangedEntryEvent<AiRequestLog>, GenericChangedEntryEvent<AiRequestLog>>,
    IAiRequestLogService
{
    public AiRequestLogService(
    Func<IAiHelperRepository> repositoryFactory,
    IPlatformMemoryCache platformMemoryCache,
    IEventPublisher eventPublisher
    ) : base(repositoryFactory, platformMemoryCache, eventPublisher)
    {
    }

    public virtual async Task<AiRequestLog> SaveRequestLog(AiRequestLog aiRequestLog)
    {
        await SaveChangesAsync([aiRequestLog]);
        return aiRequestLog;
    }

    protected override async Task<IList<AiRequestLogEntity>> LoadEntities(IRepository repository, IList<string> ids, string responseGroup)
    {
        return await ((IAiHelperRepository)repository).GetAiRequestLogsByIds(ids.ToArray(), responseGroup);
    }
}
