using System.Threading.Tasks;
using EmploymentSystem.ApplicationService.Commons.Bases;
using EmploymentSystem.Domain.Entities;

namespace EmploymentSystem.Infrastructure.Repositories.VacencyRepository
{
    public interface IVacancyRepository
    {
        Task<BaseResponse<bool>> AddVacancyAsync(Vacancy vacancy, CancellationToken cancellationToken);
        Task<BaseResponse<bool>> UpdateVacancyAsync(Vacancy vacancy, CancellationToken cancellationToken);
        Task<BaseResponse<bool>> DeleteVacancyAsync(Guid id, CancellationToken cancellationToken);
        Task<BaseResponse<IEnumerable<Vacancy>>> GetAllVacanciesAsync(CancellationToken cancellationToken);
        Task<BaseResponse<Vacancy>> GetVacancyByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<BaseResponse<IEnumerable<Vacancy>>> GetActiveVacanciesAsync(CancellationToken cancellationToken);
        Task<BaseResponse<IEnumerable<Application>>> GetApplicationsByVacancyIdAsync(Guid vacancyId, CancellationToken cancellationToken);
    }
}
