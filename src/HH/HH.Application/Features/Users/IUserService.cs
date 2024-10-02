using FluentResults;
using HH.Domain.Entitties;

namespace HH.Application.Features.Users
{
    public interface IUserService
    {
        Task<Result<User>> GetById(Guid id, CancellationToken cancellationToken = default);
    }
}