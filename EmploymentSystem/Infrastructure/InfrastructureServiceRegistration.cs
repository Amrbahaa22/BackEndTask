using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using EmploymentSystem.Domain.Entities;

namespace EmploymentSystem.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EmploymentDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))); return services;
        }
    }
}