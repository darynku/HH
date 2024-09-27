
using FluentResults;
using HH.Common.Contracts.Common;

namespace HH.Application.Features.Vacancies.Apply
{
    public record ApplyVacancyCommand(
        Guid UserId, 
        Guid VacancyId,
        DateTime ApplyDate,
        string Recomendation) : ICommand<Result<Guid>>;
}
