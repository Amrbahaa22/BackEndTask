using FluentValidation;

namespace EmploymentSystem.ApplicationService.Commands.VacancyCommands.UpdateVacancyCommand
{
    public class UpdateVacancyValidator: AbstractValidator<UpdateVacancyCommand>
    {
        public UpdateVacancyValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
        }
        
    }
}
