using EmploymentSystem.App.Commands;
using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Infrastructure;
using MediatR;

namespace EmploymentSystem.App.Handlers
{
    public class CreateVacancyCommandHandler : IRequestHandler<CreateVacancyCommand, Vacancy>
    {
        private readonly EmploymentDbContext _context;

        public CreateVacancyCommandHandler(EmploymentDbContext context)
        {
            _context = context;
        }

        public async Task<Vacancy> Handle(CreateVacancyCommand request, CancellationToken cancellationToken)
        {
            var vacancy = new Vacancy
            {
                EmployerId = request.EmployerId,
                Title = request.Title,
                Description = request.Description,
                MaxApplications = request.MaxApplications,
                ExpiryDate = request.ExpiryDate,
                IsActive = true
            };

            _context.Vacancies.Add(vacancy);
            await _context.SaveChangesAsync(cancellationToken);

            return vacancy;
        }
    }
}
