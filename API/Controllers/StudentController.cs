using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            try
            {
                return await _studentService.GetAllStudentsAsync();
            }
            catch (Exception ex)
            {
                throw new ResourceNotFoundException("No students found", 404, ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<Student> GetStudent(int id)
        {
            try
            {
                var student = await _studentService.GetStudentByIdAsync(id);
                if (student == null)
                    throw new ResourceNotFoundException($"Student with id {id} not found", 404);

                return student;
            }
            catch (ResourceNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ResourceNotFoundException("Error fetching student", 404, ex);
            }
        }

        [HttpPost]
        public async Task<Student> AddStudent(Student student)
        {
            try
            {
                return await _studentService.AddStudentAsync(student);
            }
            catch (Exception ex)
            {
                throw new ResourceNotFoundException("Error adding student", 404, ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<Student> UpdateStudent(int id, Student student)
        {
            try
            {
                student.Id = id;
                var updated = await _studentService.UpdateStudentAsync(student);
                if (updated == null)
                    throw new ResourceNotFoundException($"Student with id {id} not found", 404);

                return updated;
            }
            catch (ResourceNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ResourceNotFoundException("Error updating student", 404, ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteStudent(int id)
        {
            try
            {
                var deleted = await _studentService.DeleteStudentAsync(id);
                if (!deleted)
                    throw new ResourceNotFoundException($"Student with id {id} not found", 404);

                return deleted;
            }
            catch (ResourceNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ResourceNotFoundException("Error deleting student", 404, ex);
            }
        }
    }

    // Custom Exception
    public class ResourceNotFoundException : Exception
    {
        public int StatusCode { get; set; }

        public ResourceNotFoundException(string message, int statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public ResourceNotFoundException(string message, int statusCode, Exception inner)
            : base(message, inner)
        {
            StatusCode = statusCode;
        }
    }
}