using HH.Domain.Entitties;
using HH.Common.Contracts.Handlers;
using FluentResults;
using HH.Application.UnitOfWork;
using HH.Domain.Entitties.ValueObjects;
using HH.Application.Features.Users;

namespace HH.Application.Features.Vacancies.Create
{
    public class CreateVacancyCommandHandler : ICommandHandler<CreateVacancyCommand, Result<Guid>>
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateVacancyCommandHandler(IVacancyRepository vacancyRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _vacancyRepository = vacancyRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CreateVacancyCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.UserId, cancellationToken);

            if (user.IsFailed)
                return Result.Fail("пользователь не найден");

            var salaryRange = SalaryRange.Create(request.MinSalary, request.MaxSalary);

            var vacancy = Vacancy.Create(
                Guid.NewGuid(),
                [],
                request.Title,
                request.Description,
                salaryRange.Value,
                request.PostedDate,
                user.Value.Id,
                request.Region,
                request.Position,
                request.WorkExpirience,
                request.ExpirationDate);

            if (vacancy.IsFailed)
                return vacancy.ToResult();

            await _vacancyRepository.AddAsync(vacancy.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return vacancy.Value.Id;
        }

    }
}
