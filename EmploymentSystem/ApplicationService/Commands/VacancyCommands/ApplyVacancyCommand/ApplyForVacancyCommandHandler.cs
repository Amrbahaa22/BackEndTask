using AutoMapper;
using Azure.Core;
using EmploymentSystem.ApplicationService.Commons.Bases;
using EmploymentSystem.Domain.DTO.VacanyDto;
using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Infrastructure.Interfaces;
using EmploymentSystem.Infrastructure.Repositories.ApplicantRepository;
using EmploymentSystem.Infrastructure.Repositories.EmployerRepository;
using EmploymentSystem.Infrastructure.Repositories.UserRepository;
using EmploymentSystem.Infrastructure.Repositories.VacencyRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmploymentSystem.ApplicationService.Commands.VacancyCommands.ApplyVacancyCommand
{
    public class ApplyForVacancyCommandHandler(ICurrentUserService currentUserService, IMapper mapper, IVacancyRepository vacancyRepository, IUserRepository userRepository, IApplicantRepository applicantRepository) : IRequestHandler<ApplyForVacancyCommand, BaseResponse<bool>>
    {
        private readonly IVacancyRepository _vacancyRepository = vacancyRepository ?? throw new ArgumentNullException(nameof(vacancyRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        private readonly ICurrentUserService _currentUserService = currentUserService ?? throw new ArgumentException(nameof(currentUserService));
        private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentException(nameof(userRepository));
        private readonly IApplicantRepository _applicantRepository = applicantRepository ?? throw new ArgumentException(nameof(applicantRepository));

        public async Task<BaseResponse<bool>> Handle(ApplyForVacancyCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                Guid userId = _currentUserService.UserId;
                Guid vacancyId = command.VacancyId;
                var resVacancy = await _vacancyRepository.GetVacancyByIdAsync(vacancyId, cancellationToken);
                Vacancy vacancy = resVacancy.Data ?? throw new ArgumentException("Vacancy not found");

                if (vacancy == null || !vacancy.IsActive || vacancy.ExpiryDate < DateTime.UtcNow)
                {
                    response.Succcess = false;
                    response.Message = "Vacancy not found, inactive, or expired";

                    return response;
                }

                var resUser = await _userRepository.GetUserByIdAsync(userId, cancellationToken) ?? throw new Exception("Applicant ID not found ");
                User user = resUser.Data ?? throw new Exception("Applicant  not found ");

                var today = DateTime.UtcNow.Date;
                var applicantId = _currentUserService.UserId;
                var applications = vacancy.Applications;
                var applicationsToday = applications.Where(a => a.Applicant.Id == applicantId && a.AppliedDate >= today).Count();

                if (applicationsToday >= 1)
                {
                    response.Succcess = false;
                    response.Message = "Applicant has already applied for a vacancy today";
                    return response;
                }

                if (applications.Count >= vacancy.MaxApplications)
                {
                    response.Succcess = false;
                    response.Message = "Vacancy has reached the maximum number of applications";
                    return response;
                }
                var resApplicant = await _applicantRepository.GetApplicantByIdAsync(userId, cancellationToken) ?? throw new Exception("Applicant not found ");
                var applicant = resApplicant.Data ?? throw new Exception("Applicant not found ");

                var application = new Application
                {
                    Id = Guid.NewGuid(),
                    Vacancy = vacancy,
                    Applicant = applicant,
                    AppliedDate = DateTime.UtcNow
                };
                var resApplication = _applicantRepository.AddApplicationAsync(applicant.Id, application, cancellationToken) ?? throw new Exception("Applicant not found ");
                var res = resApplicant.Data ?? throw new Exception("Applicant not found ");

                response.Succcess = true;
                response.Message = "Apply for vacancy is succeded";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
