using HH.Common.Contracts.Common;
using HH.Common.Contracts.DTO;

namespace HH.Application.Features.Vacancies.Get
{
    public record GetOnlyVacanciesQuery(int Page, int PageSize) : IQuery<IEnumerable<VacancyDto>>;
}
