using static System.Net.Mime.MediaTypeNames;

namespace EmploymentSystem.Domain.Entities
{
    public class Vacancy
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public int MaxApplications { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        public required Employer Employer { get; set; }
        public ICollection<Application> Applications { get; set; } = [];
    }
}
