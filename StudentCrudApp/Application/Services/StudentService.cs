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
            var student = await _repository.GetByIdAsync(id);
            if (student == null) return null;

            return MapToResponseDto(student);
        }

        public async Task<IEnumerable<StudentResponseDto>> GetAllStudentsAsync()
        {
            var students = await _repository.GetAllAsync();
            return students.Select(MapToResponseDto);
        }

        public async Task<StudentResponseDto> CreateStudentAsync(CreateStudentDto dto)
        {
            if (await _repository.ExistsByEmailAsync(dto.Email))
                throw new InvalidOperationException("Email already exists");

            var student = new Student
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Course = dto.Course,
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

            student.Name = dto.Name;
            student.Email = dto.Email;
            student.Phone = dto.Phone;
            student.Course = dto.Course;
            student.DateOfBirth = dto.DateOfBirth;
            student.UpdatedAt = DateTime.UtcNow;

            var updatedStudent = await _repository.UpdateAsync(student);
            await _repository.SaveChangesAsync();

            return MapToResponseDto(updatedStudent);
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _repository.GetByIdAsync(id);
            if (student == null)
                throw new KeyNotFoundException("Student not found");

            var result = await _repository.DeleteAsync(id);
            if (result)
                await _repository.SaveChangesAsync();

            return result;
        }

        private static StudentResponseDto MapToResponseDto(Student student)
        {
            return new StudentResponseDto
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
}
