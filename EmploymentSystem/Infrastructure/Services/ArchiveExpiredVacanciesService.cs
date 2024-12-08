using EmploymentSystem.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace EmploymentSystem.Infrastructure.Services
{
    public class ArchiveExpiredVacanciesService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ArchiveExpiredVacanciesService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var expiredVacancies = await context.Vacancies
                        .Where(v => v.ExpiryDate < DateTime.UtcNow && v.IsActive)
                        .ToListAsync(stoppingToken);

                    foreach (var vacancy in expiredVacancies)
                    {
                        vacancy.IsActive = false;
                        context.Vacancies.Update(vacancy);
                    }

                    await context.SaveChangesAsync(stoppingToken);
                }

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken); // Run daily
            }
        }
    }
}
