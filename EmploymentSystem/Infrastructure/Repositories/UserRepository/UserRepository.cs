using AutoMapper;
using EmploymentSystem.ApplicationService.Commons.Bases;
using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace EmploymentSystem.Infrastructure.Repositories.UserRepository

{
    public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
    {

        private readonly ApplicationDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task<BaseResponse<bool>> AddUserAsync(User user, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync(cancellationToken);

                response.Succcess = true;
                response.Message = "Create succeed!";

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
        public async Task<BaseResponse<User>> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<User>();
            try
            {
                User? user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user != null)
                {
                    User userDto = new()
                    {
                        Id = user.Id,
                        PasswordHash = user.PasswordHash,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Role = user.Role
                    };
                    response.Data = userDto;

                }
                else
                {
                    response.Data = null;

                }
                response.Succcess = true;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
        public async Task<BaseResponse<User>> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<User>();
            try
            {
                User? user = await _dbContext.Users.FindAsync([id, cancellationToken], cancellationToken: cancellationToken);
                if (user != null)
                {
                    User userDto = new()
                    {
                        Id = user.Id,
                        PasswordHash = user.PasswordHash,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Role = user.Role
                    };
                    response.Data = userDto;


                }
                else
                {
                    response.Data = null;
                    response.Succcess = false;

                    return response;
                }

                response.Succcess = true;
            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
            }

            return response;
        }
        public async Task<BaseResponse<bool>> UserExistsAsync(string email, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var isFound = await _dbContext.Users.AnyAsync(u => u.Email == email, cancellationToken);
                if (isFound)
                {
                    response.Succcess = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;

        }
    }
}
