using EmploymentSystem.ApplicationService.Commons.Bases;
using EmploymentSystem.Domain.DTO.VacanyDto;
using EmploymentSystem.Domain.Entities;
using MediatR;

namespace EmploymentSystem.ApplicationService.Commands.VacancyCommands.CreateVacancyCommand
{
    public class CreateVacancyCommand : IRequest<BaseResponse<VacancyDto>>
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DateTime ExpiryDate { get; set; }
        public int MaxApplications { get; set; }
        public bool IsActive { get; set; }
    }
}
