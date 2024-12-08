using EmploymentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EmploymentSystem.Infrastructure.DBContext
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
    {
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<User> User{ get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Employer> Employers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employer>().HasMany(e => e.Vacancies).WithOne(v => v.Employer);
            modelBuilder.Entity<Applicant>().HasMany(a => a.Applications).WithOne(ap => ap.Applicant);
            modelBuilder.Entity<Vacancy>().HasMany(v => v.Applications).WithOne(ap => ap.Vacancy);
        }
    }
}
