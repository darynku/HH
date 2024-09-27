using FluentResults;
using HH.Common.Contracts.DTO;
using HH.Domain.Entitties;

namespace HH.Domain.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetAllUserWithVacanciesAsync(int page, int pageSize, CancellationToken cancellationToken = default);
        Task AddAsync(User user, CancellationToken cancellationToken = default);
        Task<Result<User>> GetByEmail(string email, CancellationToken cancellationToken = default);
        Task<Result<User>> GetById(Guid id, CancellationToken cancellationToken = default);
    }
}