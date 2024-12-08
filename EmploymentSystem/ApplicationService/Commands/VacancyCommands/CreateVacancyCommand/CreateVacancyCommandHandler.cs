using AutoMapper;
using EmploymentSystem.ApplicationService.Commons.Bases;
using EmploymentSystem.Domain.DTO.VacanyDto;
using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Infrastructure.Interfaces;
using EmploymentSystem.Infrastructure.Repositories.EmployerRepository;
using EmploymentSystem.Infrastructure.Repositories.VacencyRepository;
using MediatR;

namespace EmploymentSystem.ApplicationService.Commands.VacancyCommands.CreateVacancyCommand
{
    public class CreateVacancyCommandHandler(ICurrentUserService currentUserService, IMapper mapper, IEmployerRepository employerRepository, IVacancyRepository vacancyRepository) : IRequestHandler<CreateVacancyCommand, BaseResponse<VacancyDto>>
    {
        private readonly IVacancyRepository _vacancyRepository = vacancyRepository ?? throw new ArgumentNullException(nameof(vacancyRepository));
        private readonly IEmployerRepository _employerRepository = employerRepository ?? throw new ArgumentNullException(nameof(employerRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        private readonly ICurrentUserService _currentUserService = currentUserService ?? throw new ArgumentException(nameof(currentUserService));

        public async Task<BaseResponse<VacancyDto>> Handle(CreateVacancyCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<VacancyDto>();
            try
            {
                Guid userId = _currentUserService.UserId;
                var res = await _employerRepository.GetEmployerByIdAsync(userId, cancellationToken) ?? throw new Exception("Employer ID not found ");
                Employer employer = res.Data ?? throw new Exception("Employer not found ");
                if (res.Succcess && res.Data != null)
                {
                   
                    var vacancyDto = _mapper.Map<Vacancy>(command);
                    var vacancy = new Vacancy
                    {
                        Id = Guid.NewGuid(),
                        Title = vacancyDto.Title,
                        Description = vacancyDto.Description,
                        MaxApplications = vacancyDto.MaxApplications,
                        ExpiryDate = vacancyDto.ExpiryDate,
                        Employer = employer,
                        IsActive = true
                    };
                    await _vacancyRepository.AddVacancyAsync(vacancy, cancellationToken);
                    response.Succcess = true;
                    response.Data = _mapper.Map<VacancyDto>(vacancy);
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
