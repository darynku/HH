using HH.Domain.Entitties.ValueObjects;
using FluentResults;
using HH.Application.DTO;
using HH.Common.Contracts.Handlers;
using HH.Application.Files;
using FileInfo = HH.Application.Files.FileInfo;

namespace HH.Application.Features.Vacancies.Get.GetFullVacancy
{
    public class GetFullVacancyQueryHandler : IQueryHandler<GetFullVacancyQuery, Result<VacancyRespones>>
    {
        private readonly IVacancyService _vacancyService;
        private readonly IMinioProvider _filesService;
        private const string BUCKET_NAME = "files";
        public GetFullVacancyQueryHandler(IVacancyService vacancyService, IMinioProvider filesService)
        {
            _vacancyService = vacancyService;
            _filesService = filesService;
        }

        public async Task<Result<VacancyRespones>> Handle(GetFullVacancyQuery request, CancellationToken cancellationToken)
        {
            var vacancyResult = await _vacancyService.GetById(request.Id, cancellationToken);
            if (vacancyResult.IsFailed)
                return vacancyResult.ToResult();

            var vacancy = vacancyResult.Value;
            var fileUrls = new List<ItemFileDto>();

         
            foreach (var file in vacancy.Files)
            {
                var fileInfos = new FileInfo(file.PathToStorage, BUCKET_NAME);
                var url = await _filesService.CreatePresignedUrl(fileInfos, 3600); 
                fileUrls.Add(new ItemFileDto { PathToStorage = url });
            }

            var response = new VacancyRespones
            {
                Title = vacancy.Title,
                Description = vacancy.Description,
                Position = vacancy.Position,
                Salary = vacancy.Salary,
                WorkExperience = vacancy.WorkExperience,
                Region = vacancy.Region,
                Views = vacancy.Views,
                PostedDate = vacancy.PostedDate,
                ApplicationsCount = vacancy.ApplicationsCount,
                Files = fileUrls // Используем список с предписанными URL
            };

            return response;
        }

    }
}
