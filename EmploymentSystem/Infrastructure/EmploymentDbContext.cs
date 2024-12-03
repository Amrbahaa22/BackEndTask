using EmploymentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmploymentSystem.Infrastructure
{
    public class EmploymentDbContext(DbContextOptions<EmploymentDbContext> options) : DbContext(options)
    {
        public required DbSet<User> Users { get; set; }
        public required DbSet<Vacancy> Vacancies { get; set; }
        public required DbSet<Application> Applications { get; set; }
    }
}
