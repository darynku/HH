using MediatR;

namespace HH.Application.Interfaces
{
    public interface IRoleAccessValidator<in TRequest> where TRequest : IBaseRequest
    {
        Task ValidateAsync(TRequest request, CancellationToken cancellationToken); 
    }
}
