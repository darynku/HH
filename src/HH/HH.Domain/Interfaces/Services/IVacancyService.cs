using FluentResults;
using HH.Domain.Entitties;

namespace HH.Domain.Interfaces.Services
{
    public interface IVacancyService
    {
        Task<Result<Vacancy>> GetById(Guid id, CancellationToken cancellationToken = default);
        Task AddVacancyApplicationAsync(VacancyApplication vacancyApplication, CancellationToken cancellationToken = default);
    }
}