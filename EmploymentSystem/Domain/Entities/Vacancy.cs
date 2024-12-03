using static System.Net.Mime.MediaTypeNames;

namespace EmploymentSystem.Domain.Entities
{
    public class Vacancy
    {
        public int Id { get; set; }
        public int EmployerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxApplications { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }

        public List<Application> Applications { get; set; }
    }
}
