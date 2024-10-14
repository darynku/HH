using HH.Application.Interfaces;
using HH.Domain.Common;
using HH.Domain.Entitties;
using MediatR;

namespace HH.Application.Behaviours
{
    public abstract class RoleAccessValidator<TRequest> : IRoleAccessValidator<TRequest>
        where TRequest : IBaseRequest
    {
        public abstract Task ValidateAsync(TRequest request, CancellationToken cancellationToken);

        protected void ValidateRole(User user, Role role, string message)
        {
            if(user.Role == role)
                return;

            throw new Exception(message);
        }
    }
}
