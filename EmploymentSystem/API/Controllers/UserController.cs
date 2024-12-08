using EmploymentSystem.ApplicationService.Commands.UserCommands.CreateUserCommand;
using EmploymentSystem.ApplicationService.Commands.UserCommands.LoginUserCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Succcess)
            {
                return Created();
            }

            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Succcess)
            {
                return Ok(new { Token = result.Data });
            }

            return BadRequest(result);
            
        }
    }
}