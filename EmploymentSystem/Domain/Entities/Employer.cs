using EmploymentSystem.Domain.Enums;

namespace EmploymentSystem.Domain.Entities
{
    public class Employer
    {
        public Guid Id { get; set; }
        public string Role { get; set; } = "Employer";
        public ICollection<Vacancy> Vacancies { get; set; } = [];
    }
}
