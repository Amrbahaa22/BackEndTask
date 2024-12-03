using EmploymentSystem.App.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacanciesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VacanciesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVacancy(CreateVacancyCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
