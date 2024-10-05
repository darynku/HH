using FluentResults;
using HH.Application.DTO;
using HH.Common.Contracts.Common;
using HH.Domain.Entitties.ValueObjects;

namespace HH.Application.Features.Vacancies.Get.GetFullVacancy
{
    public record GetFullVacancyQuery(Guid Id) : IQuery<Result<VacancyRespones>>;
}
