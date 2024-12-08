namespace EmploymentSystem.Domain.DTO.UserDto
{
    public class CreateUserDto
    {
        public required Guid Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Role { get; set; }
    }
}
