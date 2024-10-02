using FluentResults;
using HH.Domain.Entitties;

namespace HH.Application.Features.Vacancies
{
    public interface IVacancyService
    {
        Task<Result<Vacancy>> GetById(Guid id, CancellationToken cancellationToken = default);
        Task AddVacancyApplicationAsync(VacancyApplication vacancyApplication, CancellationToken cancellationToken = default);
    }
}