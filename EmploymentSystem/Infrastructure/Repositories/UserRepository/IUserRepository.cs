using EmploymentSystem.ApplicationService.Commons.Bases;
using EmploymentSystem.Domain.Entities;

namespace EmploymentSystem.Infrastructure.Repositories.UserRepository
{

    public interface IUserRepository
    {
        Task<BaseResponse<bool>> AddUserAsync(User user, CancellationToken cancellationToken);
        Task<BaseResponse<User>> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
        Task<BaseResponse<User>> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<BaseResponse<bool>> UserExistsAsync(string email, CancellationToken cancellationToken);
    }

}
