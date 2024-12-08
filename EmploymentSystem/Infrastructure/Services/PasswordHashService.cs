using EmploymentSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EmploymentSystem.Infrastructure.Services
{
    public class PasswordHasherService : IPasswordHasher<User>
    {
        private readonly PasswordHasher<User> _passwordHasher;
        public PasswordHasherService()
        {
            _passwordHasher = new PasswordHasher<User>();
        }
        public string HashPassword(User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }
        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword) { 
            return _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword); 
        }
    }
}
