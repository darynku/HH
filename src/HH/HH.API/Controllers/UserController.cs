using HH.Application.Features.Users;
using HH.Application.Features.Users.Get;
using HH.Application.Features.Users.Login;
using HH.Application.Features.Users.Register.Boss;
using HH.Application.Features.Users.Register.Rab;
using HH.Infrastructure.Database;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace HH.API.Controllers
{
    public class UserController : ApplicationController
    {
        private readonly ISender _sender;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _contextAccessor;
    
        public UserController(ISender sender, IUserRepository userRepository, IHttpContextAccessor contextAccessor)
        {
            _sender = sender;
            _userRepository = userRepository;
            _contextAccessor = contextAccessor;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] GetUserWithVacanciesQuery query, CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(query, cancellationToken));
        }


     
        [HttpPost("registerRab")]
        public async Task<IActionResult> RegisterRab(RegisterRabCommamd commamd, CancellationToken ct)
        {
            return ValidationActionResult(await _sender.Send(commamd, ct));
        }
        [HttpPost("registerBoss")]
        public async Task<IActionResult> RegisterBoss(RegisterBossCommamd commamd, CancellationToken ct)
        {
            return ValidationActionResult(await _sender.Send(commamd, ct));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand commamd, CancellationToken ct)
        {
            var result = await _sender.Send(commamd, ct);
            if (result.IsFailed)
                return BadRequest(result.ToResult());
            return Ok(result.Value);
        }
    }
}
