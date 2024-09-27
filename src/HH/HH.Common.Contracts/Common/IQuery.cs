using MediatR;

namespace HH.Common.Contracts.Common
{
    public interface IQuery<TRespones> :  IRequest<TRespones>;
}
