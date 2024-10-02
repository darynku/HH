using FluentResults;
using HH.Common.Contracts.Handlers;
using HH.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace HH.Application.Features.Users.Login
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, Result<UserRespones>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IHttpContextAccessor _contextAccessor;
        public LoginCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider, IHttpContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _contextAccessor = contextAccessor;
        }

        public async Task<Result<UserRespones>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmail(request.Email, cancellationToken);

            if (user.IsFailed)
                return Result.Fail($"Пользователя нет с такой почтой {user.Value.Email}");

            bool verify = BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.Value.PasswordHash);
            if (!verify)
                return Result.Fail("Неправильный пароль");

            string token = _jwtProvider.Generate(user.Value);

            

            var respones = new UserRespones(user.Value.Id, token);
            return respones;

        }
    }
}
