using FluentValidation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EmploymentSystem.ApplicationService.Commands.VacancyCommands.DeactivateVacancyCommand
{
    public class DeactivateVacancyValidator : AbstractValidator<DeactivateVacancyCommand>
    {
        public DeactivateVacancyValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
        }
    }
}
