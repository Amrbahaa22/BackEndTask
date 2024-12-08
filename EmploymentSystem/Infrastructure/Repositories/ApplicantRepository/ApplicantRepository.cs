using EmploymentSystem.ApplicationService.Commons.Bases;
using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace EmploymentSystem.Infrastructure.Repositories.ApplicantRepository
{
    public class ApplicantRepository(ApplicationDbContext dbContext) : IApplicantRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task<BaseResponse<bool>> AddApplicantAsync(Applicant applicant, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                _dbContext.Applicants.Add(applicant);
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

        public async Task<BaseResponse<bool>> DeleteApplicantAsync(Guid employerId, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var applicant = await _dbContext.Applicants.FirstOrDefaultAsync(v => v.Id == employerId, cancellationToken);
                if (applicant != null)
                {
                    _dbContext.Applicants.Remove(applicant);
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
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<Applicant>> GetApplicantByIdAsync(Guid applicantId, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<Applicant>();
            try
            {
                Applicant? applicant = await _dbContext.Applicants.FirstOrDefaultAsync(v => v.Id == applicantId, cancellationToken);
                if (applicant != null)
                {
                    response.Succcess = true;
                    response.Data = applicant;
                }


            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> UpdateApplicantAsync(Applicant applicant, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                _dbContext.Applicants.Update(applicant);
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
        public async Task<BaseResponse<bool>> AddApplicationAsync(Guid applicantId, Application application, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                Applicant? applicant = await _dbContext.Applicants.FirstOrDefaultAsync(v => v.Id == applicantId, cancellationToken);

                if (applicant != null)
                {
                    applicant.Applications.Add(application);
                    _dbContext.Applicants.Update(applicant);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    response.Succcess = true;
                    response.Message = "Add Succeed!";
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
