using EmploymentSystem.ApplicationService.Commons.Bases;
using EmploymentSystem.Domain.Entities;

namespace EmploymentSystem.Infrastructure.Repositories.ApplicantRepository
{
    public interface IApplicantRepository
    {
        Task<BaseResponse<Applicant>> GetApplicantByIdAsync(Guid applicantId, CancellationToken cancellationToken);
        Task<BaseResponse<bool>> AddApplicantAsync(Applicant applicant, CancellationToken cancellationToken);
        Task<BaseResponse<bool>> UpdateApplicantAsync(Applicant applicant, CancellationToken cancellationToken);
        Task<BaseResponse<bool>> DeleteApplicantAsync(Guid applicantId, CancellationToken cancellationToken);
        Task<BaseResponse<bool>> AddApplicationAsync(Guid applicantId, Application application, CancellationToken cancellationToken);
    }
}
