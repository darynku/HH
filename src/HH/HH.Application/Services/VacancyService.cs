using FluentResults;
using HH.Application.Features.Vacancies;
using HH.Domain.Entitties;

namespace HH.Application.Services
{
    public class VacancyService : IVacancyService
    {
        private readonly IVacancyRepository _vacancyRepository;

        public VacancyService(IVacancyRepository vacancyRepository)
        {
            _vacancyRepository = vacancyRepository;
        }

        public async Task AddVacancyApplicationAsync(VacancyApplication vacancyApplication, CancellationToken cancellationToken = default)
        {
            await _vacancyRepository.AddVacancyApplicationAsync(vacancyApplication, cancellationToken);
        }

        public async Task<Result<Vacancy>> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var vacancy = await _vacancyRepository.GetById(id, cancellationToken);
            if (vacancy.IsFailed)
                return vacancy.ToResult();
            return vacancy;
        }
    }
}
