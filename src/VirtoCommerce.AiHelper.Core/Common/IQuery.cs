using MediatR;

namespace VirtoCommerce.AiHelper.Core.Common;
public interface IQuery<out TResult> : IRequest<TResult>
{
}
