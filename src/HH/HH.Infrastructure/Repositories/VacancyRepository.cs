using FluentResults;
using HH.Application.DTO;
using HH.Application.Features.Vacancies;
using HH.Domain.Entitties;
using HH.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace HH.Infrastructure.Repositories
{
    public class VacancyRepository : IVacancyRepository
    {
        private readonly ApplicationDbContext _context;

        public VacancyRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Vacancy vacancy, CancellationToken cancellationToken = default)
        {
            await _context.Vacancy.AddAsync(vacancy, cancellationToken);
        }

        public async Task AddVacancyApplicationAsync(VacancyApplication vacancyApplication, CancellationToken cancellationToken = default)
        {
            await _context.VacancyApplication.AddAsync(vacancyApplication, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<Result<Vacancy>> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var vacancy = await _context.Vacancy
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (vacancy is null)
                return Result.Fail("нет такой вакансии");

            return vacancy;
        }

        public async Task<IEnumerable<VacancyDto>> GetAll(int page, int pageSize, CancellationToken cancellationToken = default)
        {

            IQueryable<Vacancy> vacancies = _context.Vacancy;

            var result = await vacancies
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(v => new VacancyDto
                {
                    Title = v.Title,
                    Description = v.Description,
                    PostedDate = v.PostedDate,
                    Position = v.Position,
                    WorkExperience = v.WorkExperience,
                    Region = v.Region,
                    Salary = v.Salary,
                    Views = v.Views
                    
                })
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
