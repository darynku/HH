using HH.Common.Contracts.Common;
using HH.Common.Contracts.DTO;


namespace HH.Application.Features.Users.Get
{
    public record GetUserWithVacanciesQuery(int Page, int PageSize) : IQuery<IEnumerable<UserDto>>;
}
