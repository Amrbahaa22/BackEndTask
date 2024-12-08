using AutoMapper;
using EmploymentSystem.ApplicationService.Commons.Bases;
using EmploymentSystem.Domain.DTO.VacanyDto;
using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Infrastructure.Interfaces;
using EmploymentSystem.Infrastructure.Repositories.VacencyRepository;
using EmploymentSystem.Infrastructure.Services;
using MediatR;


namespace EmploymentSystem.ApplicationService.Commands.VacancyCommands.DeleteVacancyCommand
{
    public class DeleteVacancyCommandHandler(ICurrentUserService currentUserService, IVacancyRepository vacancyRepository, IMapper mapper) : IRequestHandler<DeleteVacancyCommand, BaseResponse<bool>>
    {
        private readonly IVacancyRepository _vacancyRepository = vacancyRepository;
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        private readonly ICurrentUserService _currentUserService = currentUserService ?? throw new ArgumentException(nameof(currentUserService));

        public async Task<BaseResponse<bool>> Handle(DeleteVacancyCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var vacancyDto = _mapper.Map<VacancyDto>(command);
                var res = await _vacancyRepository.GetVacancyByIdAsync(vacancyDto.Id, cancellationToken) ?? throw new ArgumentException("Vacancy not found");
                Vacancy vacancy = res.Data ?? throw new ArgumentException("Vacancy not found");

                if (vacancy.Employer.Id != _currentUserService.UserId)
                {
                    throw new UnauthorizedAccessException("You are not authorized to edit this vacancy.");
                }
                await _vacancyRepository.DeleteVacancyAsync(vacancy.Id, cancellationToken);

                response.Succcess = true;
                response.Message = "Delete Succeded";

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }

}
