using FluentValidation;

namespace EmploymentSystem.ApplicationService.Commands.VacancyCommands.DeleteVacancyCommand
{
    public class DeleteVacancyValidator: AbstractValidator<DeleteVacancyCommand>
    {
        public DeleteVacancyValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
        }
    }
}
