using StudentCrudApp.Application.DTOs;
using StudentCrudApp.Domain.Entities;
using StudentCrudApp.Domain.Interfaces;

namespace StudentCrudApp.Application.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<StudentResponseDto?> GetStudentByIdAsync(int id)
        {
            if (id <= 0) return null;

            var student = await _repository.GetByIdAsync(id);
            return student == null ? null : MapToResponseDto(student);
        }

        public async Task<IEnumerable<StudentResponseDto>> GetAllStudentsAsync()
        {
            var students = await _repository.GetAllAsync();
            return students.Select(MapToResponseDto);
        }

        public async Task<StudentResponseDto> CreateStudentAsync(CreateStudentDto dto)
        {
            var email = NormalizeEmail(dto.Email);
            if (await _repository.ExistsByEmailAsync(email))
                throw new InvalidOperationException("Email already exists");

            var student = new Student
            {
                Name = dto.Name.Trim(),
                Email = email,
                Phone = dto.Phone.Trim(),
                Course = dto.Course.Trim(),
                DateOfBirth = dto.DateOfBirth,
                CreatedAt = DateTime.UtcNow
            };

            var createdStudent = await _repository.CreateAsync(student);
            await _repository.SaveChangesAsync();
            return MapToResponseDto(createdStudent);
        }

        public async Task<StudentResponseDto> UpdateStudentAsync(UpdateStudentDto dto)
        {
            var student = await _repository.GetByIdAsync(dto.Id);
            if (student == null)
                throw new KeyNotFoundException("Student not found");

            var email = NormalizeEmail(dto.Email);
            if (!string.Equals(student.Email, email, StringComparison.OrdinalIgnoreCase) &&
                await _repository.ExistsByEmailAsync(email))
                throw new InvalidOperationException("Email already exists");

            student.Name = dto.Name.Trim();
            student.Email = email;
            student.Phone = dto.Phone.Trim();
            student.Course = dto.Course.Trim();
            student.DateOfBirth = dto.DateOfBirth;
            student.UpdatedAt = DateTime.UtcNow;

            var updatedStudent = await _repository.UpdateAsync(student);
            await _repository.SaveChangesAsync();
            return MapToResponseDto(updatedStudent);
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            if (id <= 0) return false;

            var student = await _repository.GetByIdAsync(id);
            if (student == null)
                throw new KeyNotFoundException("Student not found");

            var result = await _repository.DeleteAsync(id);
            if (result) await _repository.SaveChangesAsync();
            return result;
        }

        private static string NormalizeEmail(string email) => email.Trim().ToLowerInvariant();

        private static StudentResponseDto MapToResponseDto(Student student) => new()
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Phone = student.Phone,
            Course = student.Course,
            DateOfBirth = student.DateOfBirth,
            CreatedAt = student.CreatedAt,
            UpdatedAt = student.UpdatedAt
        };
    }
}