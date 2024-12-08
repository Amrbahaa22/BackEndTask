using EmploymentSystem.ApplicationService.Commons.Bases;
using EmploymentSystem.Domain.DTO.VacanyDto;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace EmploymentSystem.ApplicationService.Commands.VacancyCommands.UpdateVacancyCommand
{
    public class UpdateVacancyCommand : IRequest<BaseResponse<VacancyDto>>
    {
        [SwaggerSchema(ReadOnly = true)]
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public  string? Description { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? MaxApplications { get; set; }
        public bool? IsActive { get; set; }
    }
}
