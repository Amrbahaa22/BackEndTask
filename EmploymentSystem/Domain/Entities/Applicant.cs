namespace EmploymentSystem.Domain.Entities
{
    public class Applicant
    {
        public Guid Id { get; set; }
        public string Role { get; set; } = "Applicant";
        public ICollection<Application> Applications { get; set; } = [];
    }
}
