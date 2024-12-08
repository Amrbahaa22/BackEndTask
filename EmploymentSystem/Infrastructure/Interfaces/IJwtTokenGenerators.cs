using EmploymentSystem.Domain.Entities;

namespace EmploymentSystem.Infrastructure.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string userId, string role);
    }
}
