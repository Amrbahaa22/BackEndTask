using EmploymentSystem.ApplicationService.Commons.Bases;
using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Infrastructure.Interfaces;
using EmploymentSystem.Infrastructure.Repositories.UserRepository;
using EmploymentSystem.Infrastructure.Repositories.VacencyRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EmploymentSystem.ApplicationService.Commands.UserCommands.LoginUserCommand
{
    public class LoginUserCommandHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<LoginUserCommand, BaseResponse<string>>
    {
        private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));

        public async Task<BaseResponse<string>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<string>();
            try
            {
                var res = await _userRepository.GetUserByEmailAsync(command.Email, cancellationToken);
                var user = res.Data;

                if (!res.Succcess || user == null || user.PasswordHash == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, command.Password) == PasswordVerificationResult.Failed)
                {
                    throw new Exception("Email or Password is incorrect, Please try again");
                }

                response.Succcess = true;
                response.Data = _jwtTokenGenerator.GenerateToken(user.Id.ToString(), user.Role.ToString());

            }
            catch (Exception ex) { 
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
