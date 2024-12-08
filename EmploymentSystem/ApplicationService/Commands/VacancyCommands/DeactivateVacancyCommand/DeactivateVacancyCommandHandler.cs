using AutoMapper;
using EmploymentSystem.ApplicationService.Commons.Bases;
using EmploymentSystem.Domain.DTO.VacanyDto;
using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Infrastructure.Interfaces;
using EmploymentSystem.Infrastructure.Repositories.VacencyRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EmploymentSystem.ApplicationService.Commands.VacancyCommands.DeactivateVacancyCommand
{
    public class DeactivateVacancyCommandHandler(ICurrentUserService currentUserService, IVacancyRepository vacancyRepository, IMapper mapper) : IRequestHandler<DeactivateVacancyCommand, BaseResponse<bool>>
    {
        private readonly IVacancyRepository _vacancyRepository = vacancyRepository ?? throw new ArgumentNullException(nameof(vacancyRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        private readonly ICurrentUserService _currentUserService = currentUserService ?? throw new ArgumentException(nameof(currentUserService));

        public async Task<BaseResponse<bool>> Handle(DeactivateVacancyCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var vacancyDto = _mapper.Map<VacancyDto>(command);
                var res = await _vacancyRepository.GetVacancyByIdAsync(vacancyDto.Id, cancellationToken);
                
                Vacancy vacancy = res.Data ?? throw new ArgumentException("Vacancy not found");

                if (vacancy.Employer.Id != _currentUserService.UserId)
                {
                    throw new UnauthorizedAccessException("You are not authorized to edit this vacancy.");
                }

                vacancy.IsActive = false;

                await _vacancyRepository.UpdateVacancyAsync(vacancy, cancellationToken);
                response.Succcess = true;
                response.Message = "Deactivate Success";


            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;

        }
    }
}
