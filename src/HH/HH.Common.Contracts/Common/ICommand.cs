using MediatR;

namespace HH.Common.Contracts.Common
{
    public interface ICommand<TRespones> : IRequest<TRespones>;
}
