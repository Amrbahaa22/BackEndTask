using FluentValidation;

namespace EmploymentSystem.ApplicationService.Commands.VacancyCommands.ApplyVacancyCommand
{
    public class ApplyForVacancyValidator : AbstractValidator<ApplyForVacancyCommand>
    {
        public ApplyForVacancyValidator()
        {
            RuleFor(x => x.VacancyId).NotEmpty().NotNull();
        }
    }
}
