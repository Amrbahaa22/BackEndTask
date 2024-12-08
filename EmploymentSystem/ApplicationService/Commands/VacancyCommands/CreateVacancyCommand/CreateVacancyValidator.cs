
using FluentValidation;

namespace EmploymentSystem.ApplicationService.Commands.VacancyCommands.CreateVacancyCommand
{
    public class CreateVacancyValidator: AbstractValidator<CreateVacancyCommand>
    {
        public CreateVacancyValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull().MinimumLength(8).MaximumLength(100);
            RuleFor(x => x.Description).NotEmpty().NotNull();
            RuleFor(x => x.MaxApplications).NotEmpty().NotNull();
            RuleFor(x => x.ExpiryDate).NotEmpty().NotNull();
            RuleFor(x => x.IsActive).NotEmpty().NotNull();
        }
    }
}
