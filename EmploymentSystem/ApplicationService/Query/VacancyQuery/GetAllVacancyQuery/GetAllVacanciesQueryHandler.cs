using AutoMapper;
using EmploymentSystem.ApplicationService.Commons.Bases;
using EmploymentSystem.Domain.DTO.VacanyDto;
using EmploymentSystem.Infrastructure.Repositories.VacencyRepository;
using MediatR;

namespace EmploymentSystem.ApplicationService.Query.VacancyQuery.GetAllVacancyQuery
{
    public class GetAllVacancyByIdQueryHandler(IVacancyRepository vacancyRepository, IMapper mapper) : IRequestHandler<GetAllVacanciesQuery, BaseResponse<IEnumerable<VacancyDto>>>
    {
        private readonly IVacancyRepository _vacancyRepository = vacancyRepository;
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<BaseResponse<IEnumerable<VacancyDto>>> Handle(GetAllVacanciesQuery command, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<IEnumerable<VacancyDto>>();
            try
            {
                var res = await _vacancyRepository.GetAllVacanciesAsync(cancellationToken);
                var vacancies = res.Data;
                if (res.Succcess && res.Data != null)
                {
                    response.Data = _mapper.Map<List<VacancyDto>>(vacancies);
                    response.Succcess = true;
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
