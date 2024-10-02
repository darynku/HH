using FluentResults;
using HH.Application.DTO;
using HH.Domain.Entitties;

namespace HH.Application.Features.Users
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetAllUserWithVacanciesAsync(int page, int pageSize, CancellationToken cancellationToken = default);
        Task AddAsync(User user, CancellationToken cancellationToken = default);
        Task<Result<User>> GetByEmail(string email, CancellationToken cancellationToken = default);
        Task<Result<User>> GetById(Guid id, CancellationToken cancellationToken = default);
    }
}