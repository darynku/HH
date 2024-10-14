#pragma warning disable CS8613
using HH.Application.DTO;
using HH.Common.Contracts.Handlers;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace HH.Application.Features.Vacancies.Get
{
    public class GetOnlyVacanciesQueryHandler : IQueryHandler<GetOnlyVacanciesQuery, IEnumerable<VacancyDto>>
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IDistributedCache _cache;
        public GetOnlyVacanciesQueryHandler(IVacancyRepository vacancyRepository, IDistributedCache cache)
        {
            _vacancyRepository = vacancyRepository;
            _cache = cache;
        }

 
        /// <summary>
        /// Получение списка вакансикй на главной странице.
        /// </summary>
        /// <param name="request">Реквест для пагинации.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список ДТО вакансий.</returns>
        public async Task<IEnumerable<VacancyDto>?> Handle(GetOnlyVacanciesQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = $"vacancies_{request.Page}_{request.PageSize}";

            var cachedVacancies = await _cache.GetStringAsync(cacheKey, cancellationToken);

            IEnumerable<VacancyDto>? vacancies;

            if (string.IsNullOrEmpty(cachedVacancies))
            {
                vacancies = await _vacancyRepository.GetAll(request.Page, request.PageSize, cancellationToken);
                if (vacancies is null)
                    return vacancies;

                var serializedVacancies = JsonSerializer.Serialize(vacancies);

                await _cache.SetStringAsync(
                    cacheKey, 
                    serializedVacancies, 
                    new DistributedCacheEntryOptions 
                    { 
                        AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(5),
                        SlidingExpiration = TimeSpan.FromMinutes(2)
                    },
                    cancellationToken);
                return vacancies;
            }
               
            vacancies = JsonSerializer.Deserialize<IEnumerable<VacancyDto>>(cachedVacancies);
            return vacancies;
        }
    }
}
