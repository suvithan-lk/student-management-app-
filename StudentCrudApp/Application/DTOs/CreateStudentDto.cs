using System.ComponentModel.DataAnnotations;

namespace StudentCrudApp.Application.DTOs
{
    public class CreateStudentDto
    {
        [Required, StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress, StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required, Phone, StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required, StringLength(100, MinimumLength = 2)]
        public string Course { get; set; } = string.Empty;

        [Required, DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}