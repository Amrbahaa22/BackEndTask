using FluentValidation;

namespace EmploymentSystem.ApplicationService.Commands.UserCommands.CreateUserCommand
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Password).NotEmpty().NotNull().MinimumLength(8).MaximumLength(12);
            RuleFor(x => x.Email).NotEmpty().NotNull();
            RuleFor(x => x.FirstName).NotEmpty().NotNull();
            RuleFor(x => x.LastName).NotEmpty().NotNull();
            RuleFor(x => x.Role).NotEmpty().NotNull().IsInEnum();
        }
    }
}
