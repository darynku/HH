using FluentResults;
using HH.Application.DTO;
using HH.Common.Contracts.Common;
using Microsoft.AspNetCore.Http;

namespace HH.Application.Features.Vacancies.UploadFile
{
    public record UploadFileCommand(Guid VacancyId, List<UploadFileDto> Files) : ICommand<Result<Guid>>;
}
