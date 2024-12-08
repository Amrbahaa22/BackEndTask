using EmploymentSystem.ApplicationService.Commons.Bases;
using EmploymentSystem.Domain.DTO.UserDto;
using EmploymentSystem.Domain.Enums;
using MediatR;

namespace EmploymentSystem.ApplicationService.Commands.UserCommands.CreateUserCommand
{
    public class CreateUserCommand : IRequest<BaseResponse<UserResponseDto>>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required UserRole Role { get; set; }

    }
}