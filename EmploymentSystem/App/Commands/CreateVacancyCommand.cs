// Commands/CreateVacancyCommand.cs
using EmploymentSystem.Domain.Entities;
using MediatR;

namespace EmploymentSystem.App.Commands
{
    public class CreateVacancyCommand : IRequest<Vacancy>
    {
        public int EmployerId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public int MaxApplications { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
