using System.Linq;
using System.Threading;
using EmploymentSystem.ApplicationService.Commons.Bases;
using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace EmploymentSystem.Infrastructure.Repositories.VacencyRepository
{
    public class VacancyRepository(ApplicationDbContext dbContext) : IVacancyRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        public async Task<BaseResponse<bool>> AddVacancyAsync(Vacancy vacancy, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                _dbContext.Vacancies.Add(vacancy);
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
        public async Task<BaseResponse<Vacancy>> GetVacancyByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<Vacancy>();
            try
            {
                Vacancy? vacancy = await _dbContext.Vacancies.Include(v => v.Employer).FirstOrDefaultAsync(v => v.Id == id, cancellationToken: cancellationToken);
                if (vacancy != null)
                {
                    response.Data = vacancy;
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
        public async Task<BaseResponse<IEnumerable<Vacancy>>> GetActiveVacanciesAsync(CancellationToken cancellationToken)
        {
            var response = new BaseResponse<IEnumerable<Vacancy>>();
            try
            {
                var vacancy = await _dbContext.Vacancies.Where(v => v.IsActive && v.ExpiryDate > DateTime.UtcNow).ToListAsync(cancellationToken);

                if (vacancy != null)
                {
                    response.Data = vacancy;
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
        public async Task<BaseResponse<IEnumerable<Application>>> GetApplicationsByVacancyIdAsync(Guid vacancyId, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<IEnumerable<Application>>();
            try
            {
                var application = await _dbContext.Applications.Where(a => a.Vacancy.Id == vacancyId).ToListAsync(cancellationToken);

                if (application != null)
                {
                    response.Data = application;
                    response.Succcess = true;
                }
                

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<BaseResponse<bool>> UpdateVacancyAsync(Vacancy vacancy, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                _dbContext.Vacancies.Update(vacancy);
                await _dbContext.SaveChangesAsync(cancellationToken);

                response.Succcess = true;
                response.Message = "Update succeed!";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
        public async Task<BaseResponse<bool>> DeleteVacancyAsync(Guid id, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var vacancy = await _dbContext.Vacancies.FirstOrDefaultAsync(v => v.Id == id, cancellationToken: cancellationToken);
                if (vacancy != null)
                {
                    _dbContext.Vacancies.Remove(vacancy);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    response.Succcess = true;
                    response.Message = "Delete succeed!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;


        }

        public async Task<BaseResponse<IEnumerable<Vacancy>>> GetAllVacanciesAsync(CancellationToken cancellationToken)
        {
            var response = new BaseResponse<IEnumerable<Vacancy>>();
            try
            {
                var vacancies = await _dbContext.Vacancies.ToListAsync(cancellationToken);

                if (vacancies != null)
                {
                    response.Succcess = true;
                    response.Data = vacancies;
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
