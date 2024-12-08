namespace EmploymentSystem.Domain.DTO.VacanyDto
{
    public class VacancyDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int MaxApplications { get; set; }
        public bool IsActive { get; set; }
    }
}
