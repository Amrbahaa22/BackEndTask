
using EmploymentSystem.ApplicationService.Commands.VacancyCommands.ApplyVacancyCommand;
using EmploymentSystem.ApplicationService.Commands.VacancyCommands.CreateVacancyCommand;
using EmploymentSystem.ApplicationService.Commands.VacancyCommands.DeactivateVacancyCommand;
using EmploymentSystem.ApplicationService.Commands.VacancyCommands.DeleteVacancyCommand;
using EmploymentSystem.ApplicationService.Commands.VacancyCommands.UpdateVacancyCommand;
using EmploymentSystem.ApplicationService.Query.VacancyQuery.GetAllVacancyQuery;
using EmploymentSystem.ApplicationService.Query.VacancyQuery.GetVacancyByIdQuery;
using EmploymentSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacanciesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HttpPost]
        [Authorize(Policy = "EmployerPolicy")]
        public async Task<IActionResult> CreateVacancy(CreateVacancyCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Succcess)
            {
                return Created();
            }

            return BadRequest(result);
        }

        [HttpGet("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _mediator.Send(new GetAllVacanciesQuery());
            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Vacancy>> GetVacancy(Guid id)
        {
            var result = await _mediator.Send(new GetVacancyByIdQuery(id));
            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("{id}/deactivate")]
        [Authorize(Policy = "EmployerPolicy")]
        public async Task<IActionResult> DeactivateVacancy([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeactivateVacancyCommand(id));
            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "EmployerPolicy")]
        public async Task<IActionResult> DeleteVacancy([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteVacancyCommand(id));
            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVacancy([FromRoute] Guid id, [FromBody] UpdateVacancyCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);

        }

        [HttpPost("{id}/apply")]
        [Authorize(Policy = "ApplicantPolicy")]
        public async Task<IActionResult> ApplyForVacancy([FromRoute] string id)
        {
            var command = new ApplyForVacancyCommand { VacancyId = Guid.Parse(id)};
            var result = await _mediator.Send(command);
            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
