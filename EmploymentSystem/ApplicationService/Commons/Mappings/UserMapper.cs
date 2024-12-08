using AutoMapper;
using EmploymentSystem.ApplicationService.Commands.UserCommands.CreateUserCommand;
using EmploymentSystem.Domain.DTO.UserDto;
using EmploymentSystem.Domain.Entities;

namespace EmploymentSystem.ApplicationService.Commons.Mappings
{
    public class UserMapper:Profile
    {
        public UserMapper() {
            CreateMap<UserResponseDto, User>().ReverseMap();
            CreateMap<User, UserResponseDto>().ReverseMap();
            CreateMap<User, Employer>().ReverseMap();
            CreateMap<CreateUserCommand, UserResponseDto>().ReverseMap();
            CreateMap<CreateUserCommand, User>().ReverseMap();
        }
    }
}
