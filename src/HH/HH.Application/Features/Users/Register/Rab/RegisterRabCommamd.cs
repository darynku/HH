using FluentResults;
using HH.Domain.Common;
using MediatR;

namespace HH.Application.Features.Users.Register.Rab
{
    public record RegisterRabCommamd(string UserName, string Email, string Password) : IRequest<Result<UserRespones>>;
}
