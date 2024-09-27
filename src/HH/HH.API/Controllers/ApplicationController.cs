using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace HH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        protected IActionResult ValidationActionResult(ResultBase result)
        {
            if (result.IsFailed)
                return BadRequest(string.Join(",", result.Reasons.Select(m => m.Message)));
            return Ok(result);
        }

    }
}
