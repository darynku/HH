

using FluentResults;
using HH.Common.Contracts.Common;

namespace HH.Application.Features.Users.Login
{
    public record LoginCommand(string Email, string Password) : ICommand<Result<UserRespones>>;
}
