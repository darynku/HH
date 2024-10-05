using HH.API.Proccesors;
using HH.Application.DTO;
using HH.Application.Features.Users.Get;
using HH.Application.Features.Vacancies;
using HH.Application.Features.Vacancies.Apply;
using HH.Application.Features.Vacancies.Create;
using HH.Application.Features.Vacancies.Get;
using HH.Application.Features.Vacancies.Get.GetFullVacancy;
using HH.Application.Features.Vacancies.UploadFile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HH.API.Controllers.Vacancy
{
    public class VacancyController : ApplicationController
    {
        private readonly ISender _sender;
        private readonly IVacancyService _vacancyService;

        public VacancyController(ISender sender, IVacancyService vacancyService)
        {
            _sender = sender;
            _vacancyService = vacancyService;
        }

        [Authorize(Roles = "Boss")]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] GetOnlyVacanciesQuery query, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateVacancyCommand command, CancellationToken ct)
        {
            return ValidationActionResult(await _sender.Send(command, ct));
        }

        [HttpPost("apply")]
        [Authorize(Roles = "Rab")]
        public async Task<IActionResult> ApplyVacancy([FromBody] ApplyVacancyCommand command, CancellationToken ct)
        {
            return ValidationActionResult(await _sender.Send(command, ct));
        }
        [HttpGet("{vacancyId:guid}")]
        public async Task<IActionResult> GetFullVacancy([FromRoute] Guid vacancyId, CancellationToken cancellationToken)
        {

            var query = new GetFullVacancyQuery(vacancyId);
            var result = await _sender.Send(query, cancellationToken);
            if (result.IsFailed)
                return NotFound(result.Reasons.Select(m => m.Message));
            return Ok(result.Value);
        }

        [HttpPost("{vacancyId:guid}/file")]
        public async Task<IActionResult> UploadFile(
            [FromRoute] Guid vacancyId,
            [FromForm] IFormFileCollection files,
            CancellationToken cancellationToken)
        {
            await using var fileProccessor = new FIleProccessor();
            var fileDtos = fileProccessor.Proccess(files);

            var command = new UploadFileCommand(vacancyId, fileDtos);

            return ValidationActionResult(await _sender.Send(command, cancellationToken));
        }
    }
}
