using EmploymentSystem.ApplicationService.Commons.Bases;
using EmploymentSystem.Domain.Entities;

namespace EmploymentSystem.Infrastructure.Repositories.EmployerRepository
{
    public interface IEmployerRepository
    {
        Task<BaseResponse<Employer>> GetEmployerByIdAsync(Guid employerId, CancellationToken cancellationToken);
        Task<BaseResponse<bool>> AddEmployerAsync(Employer employer, CancellationToken cancellationToken);
        Task<BaseResponse<bool>> UpdateEmployerAsync(Employer employer, CancellationToken cancellationToken);
        Task<BaseResponse<bool>> DeleteEmployerAsync(Guid employerId, CancellationToken cancellationToken);

    }
}
