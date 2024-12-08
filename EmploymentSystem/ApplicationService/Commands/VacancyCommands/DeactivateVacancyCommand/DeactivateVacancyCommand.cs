using EmploymentSystem.ApplicationService.Commons.Bases;
using MediatR;

namespace EmploymentSystem.ApplicationService.Commands.VacancyCommands.DeactivateVacancyCommand
{
    public class DeactivateVacancyCommand(Guid id) : IRequest<BaseResponse<bool>>
    {
        public Guid Id { get; } = id;
    }
}
