using HH.Application.DTO;
using HH.Common.Contracts.Common;


namespace HH.Application.Features.Users.Get
{
    public record GetUserWithVacanciesQuery(int Page, int PageSize) : IQuery<IEnumerable<UserDto>>;
}
