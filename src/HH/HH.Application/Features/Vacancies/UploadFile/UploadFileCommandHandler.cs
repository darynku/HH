using FluentResults;
using HH.Application.UnitOfWork;
using HH.Common.Contracts.Handlers;
using HH.Domain.Interfaces.Repository;
using HH.Domain.Interfaces.Services;
using HH.Domain.Shared.Files;
using System.Diagnostics;

namespace HH.Application.Features.Vacancies.UploadFile
{
    public class UploadFileCommandHandler : ICommandHandler<UploadFileCommand, Result<Guid>>
    {
        private readonly IMinioProvider _minio;
        private readonly IVacancyService _vacancyService;
        private readonly IUnitOfWork _unitOfWork;

        public UploadFileCommandHandler(IMinioProvider minio, IVacancyService vacancyService, IUnitOfWork unitOfWork)
        {
            _minio = minio;
            _vacancyService = vacancyService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var vacancy = await _vacancyService.GetById(request.VacancyId, cancellationToken);
            if (vacancy.IsFailed)
                return vacancy.ToResult();
           
            var result = await _minio.UploadImage(request.File);
            if (result.IsFailed)
                return result.ToResult();

            vacancy.Value.UpdateFile(result.Value);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return vacancy.Value.Id;
        }
    }
}
