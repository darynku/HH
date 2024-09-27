using FluentResults;
using HH.Domain.Entitties;
using HH.Domain.Interfaces.Repository;
using HH.Domain.Interfaces.Services;

namespace HH.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<User>> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetById(id, cancellationToken);
            if (user.IsFailed)
                return user.ToResult();
            return user;
        }
    }
}
