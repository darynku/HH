using HH.Application.DTO;
using HH.Common.Contracts.Common;

namespace HH.Application.Features.Vacancies.Get.GetFullVacancy
{
    public record GetFullVacancyQuery() : IQuery<VacancyDto>;
}
