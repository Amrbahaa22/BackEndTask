using EmploymentSystem.ApplicationService.Commons.Bases;
using EmploymentSystem.Domain.DTO.VacanyDto;
using MediatR;

namespace EmploymentSystem.ApplicationService.Query.VacancyQuery.GetVacancyByIdQuery
{
    public class GetVacancyByIdQuery(Guid Id) : IRequest<BaseResponse<VacancyDto>>
    {
        public Guid Id { get; set; } = Id;
    }

}
