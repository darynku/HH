using FluentResults;
using HH.Common.Contracts.Common;
using Microsoft.AspNetCore.Http;

namespace HH.Application.Features.Vacancies.UploadFile
{
    public record UploadFileCommand(Guid VacancyId, IFormFile File) : ICommand<Result<Guid>>;
}
