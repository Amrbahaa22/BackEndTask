using EmploymentSystem.ApplicationService.Commons.Bases;
using MediatR;

namespace EmploymentSystem.ApplicationService.Commands.VacancyCommands.DeleteVacancyCommand
{
    public class DeleteVacancyCommand(Guid id): IRequest<BaseResponse<bool>>
    {
        public Guid Id { get; set; } = id;
    }
}
