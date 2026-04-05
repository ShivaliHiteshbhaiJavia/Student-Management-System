using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        // Match the interface return type
        public async Task<IEnumerable<Student>> GetAllAsync() =>
            await _context.Students.ToListAsync();

        public async Task<Student?> GetByIdAsync(int id) =>
            await _context.Students.FindAsync(id);

        public async Task<Student> AddAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<Student?> UpdateAsync(Student student)
        {
            var existing = await _context.Students.FindAsync(student.Id);
            if (existing == null) return null;

            existing.Name = student.Name;
            existing.Age = student.Age;
            existing.Email = student.Email;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return false;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}