using EmploymentSystem.Domain.Enums;

namespace EmploymentSystem.Infrastructure.Interfaces
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
    }
}
