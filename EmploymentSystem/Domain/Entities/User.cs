using EmploymentSystem.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace EmploymentSystem.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public override required string Email { get; set; }
        public required UserRole Role { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
