using System.Security.Claims;
using EmploymentSystem.Domain.Enums;
using EmploymentSystem.Infrastructure.Interfaces;

namespace EmploymentSystem.Infrastructure.Services
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor): ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public Guid UserId
        {
            get
            {
                var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("Token is invalid");
                return Guid.Parse(userId);
            }
        }
    }
}
