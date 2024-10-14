using FluentEmail.Core;
using FluentResults;
using HH.Application.Features.Users.Register.Rab;
using HH.Common.Contracts.Handlers;
using HH.Domain.Common;
using HH.Domain.Entitties;
using HH.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace HH.Application.Features.Users.Register.Boss
{
    public class RegisterBossCommandHandler : ICommandHandler<RegisterBossCommamd, Result<UserRespones>>
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IUserRepository _userRepository;
        private readonly IFluentEmail _fluentEmail;
        private readonly ILogger<RegisterBossCommandHandler> _logger;

        public RegisterBossCommandHandler(IJwtProvider jwtProvider, IUserRepository userRepository, IFluentEmail fluentEmail, ILogger<RegisterBossCommandHandler> logger)
        {
            _jwtProvider = jwtProvider;
            _userRepository = userRepository;
            _fluentEmail = fluentEmail;
            _logger = logger;
        }

        public async Task<Result<UserRespones>> Handle(RegisterBossCommamd request, CancellationToken cancellationToken)
        {
            var email = await _userRepository.GetByEmail(request.Email, cancellationToken);
            if (email.IsSuccess)
                return Result.Fail("email exists");

            var passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password);

            try
            {
                var user = User.CreateBoss(Guid.NewGuid(), Role.Boss, "test.jpg", request.UserName, request.Email, passwordHash);

                await _userRepository.AddAsync(user.Value, cancellationToken);
                var token = _jwtProvider.Generate(user.Value);

                

                var respones = new UserRespones(user.Value.Id, token);

                return respones;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while register user: {request.Email}", ex);
                throw;
            };
        }
    }
}
