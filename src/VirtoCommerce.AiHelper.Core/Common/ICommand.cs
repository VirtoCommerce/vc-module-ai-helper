using MediatR;

namespace VirtoCommerce.AiHelper.Core.Common;
public interface ICommand<out TResult> : IRequest<TResult>
{
}

public interface ICommand : IRequest
{
}
