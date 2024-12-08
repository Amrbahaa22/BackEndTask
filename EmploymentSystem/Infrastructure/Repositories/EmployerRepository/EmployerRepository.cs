using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Infrastructure.DBContext;
using EmploymentSystem.ApplicationService.Commons.Bases;
using Microsoft.EntityFrameworkCore;

namespace EmploymentSystem.Infrastructure.Repositories.EmployerRepository
{
    public class EmployerRepository(ApplicationDbContext dbContext) : IEmployerRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task<BaseResponse<Employer>> GetEmployerByIdAsync(Guid employerId, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<Employer>();
            try
            {
                Employer? employer = await _dbContext.Employers.FirstOrDefaultAsync(v => v.Id == employerId, cancellationToken);
                if (employer != null)
                {
                    response.Succcess = true;
                    response.Data = employer;
                }


            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> AddEmployerAsync(Employer employer, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                _dbContext.Employers.Add(employer);
                await _dbContext.SaveChangesAsync(cancellationToken);

                response.Succcess = true;
                response.Message = "Create Succeed!";

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse<bool>> UpdateEmployerAsync(Employer employer, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                _dbContext.Employers.Update(employer);
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

        public async Task<BaseResponse<bool>> DeleteEmployerAsync(Guid employerId, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var employer = await _dbContext.Employers.FirstOrDefaultAsync(v=> v.Id == employerId, cancellationToken);
                if (employer != null)
                {
                    _dbContext.Employers.Remove(employer);
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
    }
}
