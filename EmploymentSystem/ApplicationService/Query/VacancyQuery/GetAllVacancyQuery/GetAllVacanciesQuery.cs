using EmploymentSystem.ApplicationService.Commons.Bases;
using EmploymentSystem.Domain.DTO.VacanyDto;
using MediatR;

namespace EmploymentSystem.ApplicationService.Query.VacancyQuery.GetAllVacancyQuery
{
    public class GetAllVacanciesQuery : IRequest<BaseResponse<IEnumerable<VacancyDto>>>
    {
    }
}
