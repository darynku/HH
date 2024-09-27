using MediatR;

namespace HH.Common.Contracts.Handlers
{
    public interface IQueryHandler<TRequest, TRespones> : IRequestHandler<TRequest, TRespones>
    where TRequest : IRequest<TRespones>;
}
