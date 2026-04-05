using System;
using System.Linq; // For FirstOrDefault
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Entities;
using API.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly JwtService _jwtService;

        public AuthController(IStudentService studentService, JwtService jwtService)
        {
            _studentService = studentService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Student student)
        {
            Console.WriteLine("AuthController: Login request received for email: " + student.Email);

            var existingStudent = _studentService.GetAllStudentsAsync().Result
                                    .FirstOrDefault(s => s.Email == student.Email);

            if (existingStudent == null)
            {
                Console.WriteLine("AuthController: Student not found");
                return Unauthorized("Student not found");
            }

            Console.WriteLine("AuthController: Generating JWT for student: " + existingStudent.Email);
            var token = _jwtService.Generate(existingStudent.Email);
            Console.WriteLine("AuthController: JWT generated: " + token);

            return Ok(new { token });
        }
    }
}