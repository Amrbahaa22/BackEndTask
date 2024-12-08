namespace EmploymentSystem.Domain.Entities
{
    public class Application
    {
        public Guid Id { get; set; }
        public required Vacancy Vacancy { get; set; }
        public required Applicant Applicant { get; set; }
        public DateTime AppliedDate { get; set; }
    }
}
