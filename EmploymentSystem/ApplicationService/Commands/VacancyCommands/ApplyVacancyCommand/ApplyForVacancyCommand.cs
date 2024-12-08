using EmploymentSystem.ApplicationService.Commons.Bases;
using MediatR;

namespace EmploymentSystem.ApplicationService.Commands.VacancyCommands.ApplyVacancyCommand
{
    public class ApplyForVacancyCommand() : IRequest<BaseResponse<bool>>
    {
        public Guid VacancyId { get; set; }
    }
}
