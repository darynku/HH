using FluentResults;
using HH.Domain.Entitties;

namespace HH.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<Result<User>> GetById(Guid id, CancellationToken cancellationToken = default);
    }
}