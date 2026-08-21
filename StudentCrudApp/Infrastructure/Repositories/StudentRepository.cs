using Microsoft.EntityFrameworkCore;
using StudentCrudApp.Domain.Entities;
using StudentCrudApp.Domain.Interfaces;
using StudentCrudApp.Infrastructure.Data;

namespace StudentCrudApp.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDbContext _context;

        public StudentRepository(StudentDbContext context)
        {
            _context = context;
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> CreateAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            return student;
        }

        public Task<Student> UpdateAsync(Student student)
        {
            _context.Students.Update(student);
            return Task.FromResult(student);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await GetByIdAsync(id);
            if (student == null) return false;

            _context.Students.Remove(student);
            return true;
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Students.AnyAsync(s => s.Email == email);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
