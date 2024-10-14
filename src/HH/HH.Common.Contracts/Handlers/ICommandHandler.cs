using MediatR;

namespace HH.Common.Contracts.Handlers
{
    public interface ICommandHandler<in TRequest> : IRequestHandler<TRequest> 
     where TRequest : IRequest;

    public interface ICommandHandler<in TRequest, TRespones> : IRequestHandler<TRequest, TRespones>
        where TRequest : IRequest<TRespones>;
}
