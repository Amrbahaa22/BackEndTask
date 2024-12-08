using EmploymentSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using EmploymentSystem.ApplicationService.Commons.Bases;
using AutoMapper;
using EmploymentSystem.Infrastructure.Repositories.EmployerRepository;
using EmploymentSystem.Domain.Enums;
using EmploymentSystem.Domain.DTO.UserDto;
using EmploymentSystem.Infrastructure.Repositories.ApplicantRepository;

namespace EmploymentSystem.ApplicationService.Commands.UserCommands.CreateUserCommand
{
    public class CreateUserCommandHandler(UserManager<User> userManager, IPasswordHasher<User> passwordHasher, IEmployerRepository employerRepository, IApplicantRepository applicantRepository, IMapper mapper) : IRequestHandler<CreateUserCommand, BaseResponse<UserResponseDto>>
    {
        private readonly UserManager<User> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        private readonly IEmployerRepository _employerRepository = employerRepository ?? throw new ArgumentNullException(nameof(employerRepository));
        private readonly IApplicantRepository _applicantRepository = applicantRepository ?? throw new ArgumentNullException(nameof(applicantRepository));
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        public async Task<BaseResponse<UserResponseDto>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<UserResponseDto>();
            try
            {
                var user = _mapper.Map<User>(command);

                user.Id = Guid.NewGuid();

                user.PasswordHash = _passwordHasher.HashPassword(user, command.Password);
                user.Email = user.Email;
                user.UserName = user.Email;

                var result = await _userManager.CreateAsync(user);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                }

                if (user.Role == UserRole.Employer)
                {
                    Employer employer = new()
                    {
                        Id = user.Id,
                    };
                    await _employerRepository.AddEmployerAsync(employer, cancellationToken);
                }
                else
                {
                    Applicant applicant = new()
                    {
                        Id = user.Id,
                    };
                    await _applicantRepository.AddApplicantAsync(applicant, cancellationToken);

                }

                response.Succcess = true;
                response.Data = _mapper.Map<UserResponseDto>(user);

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;


        }
    }

}
