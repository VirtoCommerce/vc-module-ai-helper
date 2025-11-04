using MediatR;

namespace VirtoCommerce.AiHelper.Core.Common;
public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
}
