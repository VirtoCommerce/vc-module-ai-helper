using System;
using System.ComponentModel.DataAnnotations;
using VirtoCommerce.AiHelper.Core.Models;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Domain;

namespace VirtoCommerce.AiHelper.Data.Models;
public class AiRequestLogEntity : AuditableEntity, IDataEntity<AiRequestLogEntity, AiRequestLog>
{
    [StringLength(128)]
    public string ProviderName { get; set; }

    [StringLength(128)]
    public string Model { get; set; }

    [StringLength(128)]
    public string RequestType { get; set; }

    [StringLength(128)]
    public string TaskType { get; set; }

    [StringLength(128)]
    public string UserId { get; set; }

    [StringLength(128)]
    public string EntityId { get; set; }

    [StringLength(128)]
    public string EntityType { get; set; }

    public int RequestDuration { get; set; }
    public bool IsSuccess { get; set; }

    public string RequestContext { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string ErrorText { get; set; }

    public AiRequestLog ToModel(AiRequestLog model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        model.Id = Id;
        model.CreatedBy = CreatedBy;
        model.CreatedDate = CreatedDate;
        model.ModifiedBy = ModifiedBy;
        model.ModifiedDate = ModifiedDate;

        model.ProviderName = ProviderName;
        model.Model = Model;
        model.RequestType = RequestType;
        model.TaskType = TaskType;
        model.UserId = UserId;
        model.EntityId = EntityId;
        model.EntityType = EntityType;
        model.RequestDuration = RequestDuration;
        model.IsSuccess = IsSuccess;

        model.RequestContext = RequestContext;
        model.Prompt = Prompt;
        model.Response = Response;
        model.ErrorText = ErrorText;

        return model;
    }

    public AiRequestLogEntity FromModel(AiRequestLog model, PrimaryKeyResolvingMap pkMap)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        pkMap.AddPair(model, this);

        Id = model.Id;
        CreatedBy = model.CreatedBy;
        CreatedDate = model.CreatedDate;
        ModifiedBy = model.ModifiedBy;
        ModifiedDate = model.ModifiedDate;

        ProviderName = model.ProviderName;
        Model = model.Model;
        RequestType = model.RequestType;
        TaskType = model.TaskType;
        UserId = model.UserId;
        EntityId = model.EntityId;
        EntityType = model.EntityType;
        RequestDuration = model.RequestDuration;
        IsSuccess = model.IsSuccess;

        RequestContext = model.RequestContext;
        Prompt = model.Prompt;
        Response = model.Response;
        ErrorText = model.ErrorText;

        return this;
    }

    public void Patch(AiRequestLogEntity target)
    {
        if (target == null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        target.ProviderName = ProviderName;
        target.Model = Model;
        target.RequestType = RequestType;
        target.TaskType = TaskType;
        target.UserId = UserId;
        target.EntityId = EntityId;
        target.EntityType = EntityType;
        target.RequestDuration = RequestDuration;
        target.IsSuccess = IsSuccess;

        target.RequestContext = RequestContext;
        target.Prompt = Prompt;
        target.Response = Response;
        target.ErrorText = ErrorText;
    }
}
