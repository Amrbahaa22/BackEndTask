using System.Diagnostics;
using AutoMapper;
using EmploymentSystem.ApplicationService.Commons.Bases;
using EmploymentSystem.Domain.DTO.VacanyDto;
using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Infrastructure.Repositories.VacencyRepository;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EmploymentSystem.ApplicationService.Commands.VacancyCommands.UpdateVacancyCommand
{
    public class UpdateVacancyCommandHandler(IVacancyRepository vacancyRepository, IMapper mapper) : IRequestHandler<UpdateVacancyCommand, BaseResponse<VacancyDto>>
    {
        private readonly IVacancyRepository _vacancyRepository = vacancyRepository;
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<BaseResponse<VacancyDto>> Handle(UpdateVacancyCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<VacancyDto>();
            try
            {
                var res = await _vacancyRepository.GetVacancyByIdAsync(command.Id, cancellationToken) ?? throw new ArgumentException("Vacancy not found");
                Vacancy vacancy = res.Data ?? throw new ArgumentException("Vacancy not found");
                if (vacancy.ExpiryDate > command.ExpiryDate)
                {
                    throw new BadHttpRequestException("The new expiration date can not be earlier than the old expiration date");
                }
                if(command.MaxApplications < vacancy.Applications.Count)
                {
                    throw new BadHttpRequestException("Max application can not be less that the applied applications");
                }

                
                vacancy.Title = command.Title ?? vacancy.Title;
                vacancy.Description = command.Description ?? vacancy.Description;
                vacancy.ExpiryDate = command.ExpiryDate ?? vacancy.ExpiryDate;

                vacancy.MaxApplications = command.MaxApplications ?? vacancy.MaxApplications;
                vacancy.IsActive = command.IsActive ?? vacancy.IsActive;

                await _vacancyRepository.UpdateVacancyAsync(vacancy, cancellationToken);

                response.Succcess = true;
                response.Data = _mapper.Map<VacancyDto>(vacancy);

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Succcess = false;
            }

            return response;
        }
    }
}
