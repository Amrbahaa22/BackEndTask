using FluentValidation;

namespace EmploymentSystem.ApplicationService.Commands.UserCommands.LoginUserCommand
{
    public class LoginUserValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Password).NotEmpty().NotNull();
            RuleFor(x => x.Email).NotEmpty().NotNull();
        }

    }
}
