using EmploymentSystem.ApplicationService.Commons.Bases;
using MediatR;

namespace EmploymentSystem.ApplicationService.Commands.UserCommands.LoginUserCommand
{
    public class LoginUserCommand : IRequest<BaseResponse<string>>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
