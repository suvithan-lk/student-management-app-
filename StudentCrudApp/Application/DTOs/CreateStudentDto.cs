namespace StudentCrudApp.Application.DTOs
{
    public class CreateStudentDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Course { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }
}
