using AutoMapper;
using EmploymentSystem.ApplicationService.Commands.VacancyCommands.ApplyVacancyCommand;
using EmploymentSystem.ApplicationService.Commands.VacancyCommands.CreateVacancyCommand;
using EmploymentSystem.ApplicationService.Commands.VacancyCommands.DeactivateVacancyCommand;
using EmploymentSystem.ApplicationService.Commands.VacancyCommands.DeleteVacancyCommand;
using EmploymentSystem.ApplicationService.Commands.VacancyCommands.UpdateVacancyCommand;
using EmploymentSystem.Domain.DTO.VacanyDto;
using EmploymentSystem.Domain.Entities;

namespace EmploymentSystem.ApplicationService.Commons.Mappings
{
    public class VacancyMapper : Profile
    {
        public VacancyMapper() {
            CreateMap<VacancyDto, Vacancy>().ReverseMap();
            CreateMap<CreateVacancyCommand, Vacancy>().ReverseMap();
            CreateMap<CreateVacancyCommand, VacancyDto>().ReverseMap();
            CreateMap<UpdateVacancyCommand, Vacancy>().ReverseMap();
            CreateMap<UpdateVacancyCommand, VacancyDto>().ReverseMap();

        }
    }
}
