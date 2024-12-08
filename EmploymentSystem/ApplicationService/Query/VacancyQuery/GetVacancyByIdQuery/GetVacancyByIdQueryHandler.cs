using AutoMapper;
using Azure.Core;
using EmploymentSystem.ApplicationService.Commons.Bases;
using EmploymentSystem.ApplicationService.Query.VacancyQuery.GetAllVacancyQuery;
using EmploymentSystem.Domain.DTO.VacanyDto;
using EmploymentSystem.Infrastructure.Repositories.VacencyRepository;
using MediatR;

namespace EmploymentSystem.ApplicationService.Query.VacancyQuery.GetVacancyByIdQuery
{
    public class GetVacancyByIdQueryHandler(IVacancyRepository vacancyRepository, IMapper mapper) : IRequestHandler<GetVacancyByIdQuery, BaseResponse<VacancyDto>>
    {
        private readonly IVacancyRepository _vacancyRepository = vacancyRepository;
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<BaseResponse<IEnumerable<VacancyDto>>> Handle(GetAllVacanciesQuery command, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<IEnumerable<VacancyDto>>();
            try
            {
                var res = await _vacancyRepository.GetAllVacanciesAsync(cancellationToken);

                if (res.Succcess && res.Data != null)
                {
                    var vacancies = res.Data;
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

        public async Task<BaseResponse<VacancyDto>> Handle(GetVacancyByIdQuery command, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<VacancyDto>();
            try
            {
                var res = await _vacancyRepository.GetVacancyByIdAsync(command.Id, cancellationToken);
                if (res.Succcess && res.Data != null)
                {
                    var vacancy = res.Data;
                    response.Data = _mapper.Map<VacancyDto>(vacancy);
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
