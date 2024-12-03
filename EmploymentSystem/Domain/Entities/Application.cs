namespace EmploymentSystem.Domain.Entities
{
    public class Application
    {
        public int Id { get; set; }
        public int VacancyId { get; set; }
        public int ApplicantId { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}
