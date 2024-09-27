using FluentResults;
using HH.Application.UnitOfWork;
using HH.Common.Contracts.Handlers;
using HH.Domain.Entitties;
using HH.Domain.Interfaces.Repository;
using HH.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HH.Application.Features.Vacancies.Apply
{
    public class ApplyVacancyCommandHandler : ICommandHandler<ApplyVacancyCommand, Result<Guid>>
    {
        private readonly IUserService _userService;
        private readonly IVacancyService _vacancyService;
        private readonly IHttpContextAccessor _contextAccessor;

        public ApplyVacancyCommandHandler(
            IVacancyService vacancyService,
            IUserService userService,
            IHttpContextAccessor contextAccessor)
        {
            _vacancyService = vacancyService;
            _userService = userService;
            _contextAccessor = contextAccessor;
        }

        public async Task<Result<Guid>> Handle(ApplyVacancyCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _contextAccessor.HttpContext.User.Claims.First().Value;  

            var user = await _userService.GetById(request.UserId, cancellationToken);

   
            var vacancy = await _vacancyService.GetById(request.VacancyId, cancellationToken);

            var vacancyApplication = VacancyApplication.Create(Guid.NewGuid(), request.UserId, request.VacancyId, request.ApplyDate, request.Recomendation);

            await _vacancyService.AddVacancyApplicationAsync(vacancyApplication, cancellationToken);

            return vacancyApplication.Id;
            
        }
    }
}
