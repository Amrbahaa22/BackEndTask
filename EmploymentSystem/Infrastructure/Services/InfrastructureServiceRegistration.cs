using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Infrastructure.DBContext;

namespace EmploymentSystem.Infrastructure.Services
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
    }
}