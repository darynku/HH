using FluentResults;
using HH.Common.Contracts.Common;
using HH.Common.Contracts.DTO;
using Microsoft.AspNetCore.Http;

namespace HH.Application.Features.Vacancies.UploadFile
{
    public record UploadFileCommand(Guid VacancyId, List<UploadFileDto> Files) : ICommand<Result<Guid>>;
}
