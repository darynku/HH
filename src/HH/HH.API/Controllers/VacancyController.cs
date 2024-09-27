using HH.Application.Features.Users.Get;
using HH.Application.Features.Vacancies.Apply;
using HH.Application.Features.Vacancies.Create;
using HH.Application.Features.Vacancies.Get;
using HH.Application.Features.Vacancies.UploadFile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HH.API.Controllers
{
    public class VacancyController : ApplicationController
    {
        private readonly ISender _sender;

        public VacancyController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize(Roles = "Boss")]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] GetOnlyVacanciesQuery query, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(query, cancellationToken);
            if(result.AsEnumerable().IsNullOrEmpty())
                return NotFound();  
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

        [HttpPost("{vacancyId:guid}/file")]
        public async Task<IActionResult> UploadFile(
            IFormFile file,
            [FromRoute] Guid vacancyId,
            CancellationToken cancellationToken)
        {
            var command = new UploadFileCommand(vacancyId, file);
            var result = await _sender.Send(command, cancellationToken);
            if (result.IsFailed)
                return BadRequest(result.ToResult());
            return Ok(result);
        }
    }
}
