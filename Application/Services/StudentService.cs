using Domain.Entities;
using Domain.Interfaces;
using Application.Interfaces;

namespace Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        // Match interface return type exactly
        public async Task<IEnumerable<Student>> GetAllStudentsAsync() =>
            await _studentRepository.GetAllAsync();

        public async Task<Student?> GetStudentByIdAsync(int id) =>
            await _studentRepository.GetByIdAsync(id);

        public async Task<Student> AddStudentAsync(Student student) =>
            await _studentRepository.AddAsync(student);

        public async Task<Student?> UpdateStudentAsync(Student student) =>
            await _studentRepository.UpdateAsync(student);

        public async Task<bool> DeleteStudentAsync(int id) =>
            await _studentRepository.DeleteAsync(id);
    }
}