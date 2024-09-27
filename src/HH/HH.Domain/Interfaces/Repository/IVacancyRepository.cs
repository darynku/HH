using FluentResults;
using HH.Common.Contracts.DTO;
using HH.Domain.Entitties;

namespace HH.Domain.Interfaces.Repository
{
    public interface IVacancyRepository
    {
        Task<IEnumerable<VacancyDto>> GetAll(int page, int pageSize, CancellationToken cancellationToken = default);
        Task AddAsync(Vacancy vacancy, CancellationToken cancellationToken = default);
        Task AddVacancyApplicationAsync(VacancyApplication vacancyApplication, CancellationToken cancellationToken = default);
        Task<Result<Vacancy>> GetById(Guid id, CancellationToken cancellationToken = default);
    }
}