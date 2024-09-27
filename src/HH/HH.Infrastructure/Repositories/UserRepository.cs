using FluentResults;
using HH.Common.Contracts.DTO;

using HH.Domain.Entitties;
using HH.Domain.Interfaces.Repository;
using HH.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace HH.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Result<User>> GetByEmail(string email, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);
            
            if(user is null) 
                return Result.Fail($"Нету пользователя {email}");

            return user;
        }


        public async Task<IEnumerable<UserDto>> GetAllUserWithVacanciesAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        {
            IQueryable<User> users = _context.Users;
            var result = await users
                        .AsNoTracking()
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .Select(u => new UserDto
                        {
                            UserName = u.UserName,
                            Vacancies = u.Vacancies.Select(v => new VacancyDto 
                            {
                                Title = v.Title,
                                Description = v.Description
                            })
                        })
                        .ToListAsync(cancellationToken);
            return result;
        }
        public async Task<Result<User>> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user is null)
                return Result.Fail($"Нету пользователя {id}");
            return user;
        }
    }
}
