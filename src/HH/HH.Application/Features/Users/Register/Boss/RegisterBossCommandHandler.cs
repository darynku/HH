using FluentResults;
using HH.Application.Features.Users.Register.Rab;
using HH.Common.Contracts.Handlers;
using HH.Domain.Common;
using HH.Domain.Entitties;
using HH.Domain.Interfaces.Repository;
using HH.Domain.Interfaces.Services;

namespace HH.Application.Features.Users.Register.Boss
{
    public class RegisterBossCommandHandler : ICommandHandler<RegisterBossCommamd, Result<UserRespones>>
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IUserRepository _userRepository;

        public RegisterBossCommandHandler(IJwtProvider jwtProvider, IUserRepository userRepository)
        {
            _jwtProvider = jwtProvider;
            _userRepository = userRepository;
        }

        public async Task<Result<UserRespones>> Handle(RegisterBossCommamd request, CancellationToken cancellationToken)
        {
            var email = await _userRepository.GetByEmail(request.Email, cancellationToken);
            if (email.IsSuccess)
                return Result.Fail("email exists");

            var passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password);

            var user = User.CreateBoss(Guid.NewGuid(), Role.Boss, null, request.UserName, request.Email, passwordHash);

            await _userRepository.AddAsync(user.Value);
            var token = _jwtProvider.Generate(user.Value);
            var respones = new UserRespones(user.Value.Id, token);
            return respones;

        }
    }
}
