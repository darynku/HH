using FluentResults;
using MediatR;


namespace HH.Application.Features.Users.Register.Boss
{
    public record RegisterBossCommamd(string UserName, string Email, string Password) : IRequest<Result<UserRespones>>;
}
